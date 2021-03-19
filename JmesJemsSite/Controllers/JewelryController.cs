using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JmesJemsSite.Data;
using JmesJemsSite.Models;
using DynamicVML;
using JmesJemsSite.ViewModels;
using DynamicVML.Extensions;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace JmesJemsSite.Controllers
{
    public class JewelryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public JewelryController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }
        public IActionResult AddMaterial(AddNewDynamicItem parameters)
        {
            // This is the GET action that will be called whenever the user clicks 
            // the "Add new item" link in a DynamicList view. It accepts a an object
            // of the class ListItemParameters that contains information about where
            // the item needs to be inserted (i.e. the id of the div to where contents 
            // as well as the path to your viewmodels in the main form). All of those
            // are computed automatically from the view by the library.

            // Now here you can create another view model for your model
            var newMaterialViewModel = new MaterialViewModel()
            {
                Title = "New Material"
            };

            // Now you have to call the extension PartialView method passing the view model.
            // This is an extension method that creates a partial view with the needed HTML 
            // prefix for the fields in your form so the form will post correctly when it 
            // gets submitted.
            return this.PartialView(newMaterialViewModel, parameters);
        }
        private static JewelryViewModel ModelToViewModel(Jewelry model)
        {
            
            var jewelryViewModel = new JewelryViewModel()
            {
                JewelryId = model.ProductId,
                Title = model.Title,
                Type = model.Type,
                Color = model.Color,
                Size = model.Size,
                Price = model.Price,
                Materials = model.Materials.ToDynamicList(m => new MaterialViewModel()
                {
                    MaterialId = m.MaterialId,
                    Title = m.Title,
                    Category = m.Category
                })
            };

            return jewelryViewModel;
        }
        private static Jewelry ViewModelToModel(JewelryViewModel viewModel)
        {
            return new Jewelry()
            {
                ProductId = viewModel.JewelryId,
                Title = viewModel.Title,
                Type = viewModel.Type,
                Color = viewModel.Color,
                Size = viewModel.Size,
                Price = viewModel.Price,
                Materials = viewModel.Materials.ToModel(m => new Material
                {
                    MaterialId = m.MaterialId,
                    Title = m.Title,
                    Category = m.Category
                }).ToList()
            };
        }
        // GET: Jewelry
        public async Task<IActionResult> Jewelry()
        {
           return View(await _context.Jewelry.ToListAsync());
        }

        // GET: Jewelry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jewelry = await _context.Jewelry
                .Include(x => x.Materials)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (jewelry == null)
            {
                return NotFound();
            }

            return View(ModelToViewModel(jewelry));
        }

        // GET: Jewelry/Create
        public IActionResult Create()
        {
            return View(new JewelryViewModel());
        }

        // POST: Jewelry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JewelryViewModel jewelry)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(jewelry);

                Jewelry jewel = new Jewelry()
                {
                    Title = jewelry.Title,
                    Type = jewelry.Type,
                    Size = jewelry.Size,
                    Price = jewelry.Price,
                    JewelryImage = uniqueFileName,
                    Materials = jewelry.Materials.ToModel(m => new Material
                    {
                        MaterialId = m.MaterialId,
                        Title = m.Title,
                        Category = m.Category
                    }).ToList()
                };
                _context.Add(jewel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Jewelry));
            }
            return View();
        }

        // GET: Jewelry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var jewelry = await _context.Jewelry
                .Include(x => x.Materials)
                .FirstOrDefaultAsync(x => x.ProductId == id);
            var jewelryViewModel = new JewelryViewModel()
            {
                JewelryId = jewelry.ProductId,
                Title = jewelry.Title,
                Type = jewelry.Type,
                Color = jewelry.Color,
                Size = jewelry.Size,
                Price = jewelry.Price,
                ExistingImage = jewelry.JewelryImage,
                Materials = jewelry.Materials.ToDynamicList(m => new MaterialViewModel()
                {
                    MaterialId = m.MaterialId,
                    Title = m.Title,
                    Category = m.Category
                })
            };
            if (jewelry == null)
            {
                return NotFound();
            }
            return View(jewelryViewModel);
        }

        // POST: Jewelry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JewelryViewModel jewelry)
        {
            if (id != jewelry.JewelryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(jewelry);
                var jewelryModel = new Jewelry();

                jewelryModel.ProductId = jewelry.JewelryId;
                jewelryModel.Title = jewelry.Title;
                jewelryModel.Type = jewelry.Type;
                jewelryModel.Color = jewelry.Color;
                jewelryModel.Size = jewelry.Size;
                jewelryModel.Price = jewelry.Price;
                jewelryModel.JewelryImage = uniqueFileName;
                jewelryModel.Materials = jewelry.Materials.ToModel(m => new Material
                {
                    MaterialId = m.MaterialId,
                    Title = m.Title,
                    Category = m.Category
                }).ToList();
                if (jewelry.ProductImage == null)
                {
                    jewelryModel.JewelryImage = uniqueFileName;
                }
                else if (jewelry.ProductImage != null)
                {
                    if (jewelry.ExistingImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", jewelry.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }

                    jewelryModel.JewelryImage = UploadedFile(jewelry);
                }
                _context.Update(jewelryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Jewelry));
            }
            return View(jewelry);
        }

        // GET: Jewelry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jewelry = await _context.Jewelry
                .Include(x => x.Materials)
                .FirstOrDefaultAsync(m => m.ProductId == id);

       
            var jewelryViewModel = new JewelryViewModel()
            {
                JewelryId = jewelry.ProductId,
                Title = jewelry.Title,
                Type = jewelry.Type,
                Color = jewelry.Color,
                Size = jewelry.Size,
                Price = jewelry.Price,
                ExistingImage = jewelry.JewelryImage,
                Materials = jewelry.Materials.ToDynamicList(m => new MaterialViewModel()
                {
                    MaterialId = m.MaterialId,
                    Title = m.Title,
                    Category = m.Category
                })
            };
            if (jewelry == null)
            {
                return NotFound();
            }

            return View(jewelryViewModel);
        }

        // POST: Jewelry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jewelry = await _context.Jewelry
                .Include(x => x.Materials)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            var curImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", jewelry.JewelryImage);
            _context.Jewelry.Remove(jewelry);

            if (await _context.SaveChangesAsync() > 0)
            {
                if (System.IO.File.Exists(curImage))
                {
                    System.IO.File.Delete(curImage);
                }
            }
            return RedirectToAction(nameof(Jewelry));
        }
        private string UploadedFile(JewelryViewModel jewelry)
        {
            string uniqueFileName = null;

            if (jewelry.ProductImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + jewelry.ProductImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    jewelry.ProductImage.CopyTo(fileStream);

                }
            }
            return uniqueFileName;
        }

        private bool JewelryExists(int id)
        {
            return _context.Jewelry.Any(e => e.ProductId == id);
        }
    }
}
