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
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace JmesJemsSite.Controllers
{
    
    public class ArtworkController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ArtworkController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
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
        private ArtworkViewModel ModelToViewModel(Artwork model)
        {
            var artworkViewModel = new ArtworkViewModel()
            {

                ArtId = model.ProductId,
                Title = model.Title,
                Type = model.Type,
                Length = model.Length,
                Width = model.Width,
                Price = model.Price,
                Materials = model.Materials.ToDynamicList(m => new MaterialViewModel()
                {
                    MaterialId = m.MaterialId,
                    Title = m.Title,
                    Category = m.Category
                })
            };

            return artworkViewModel;
        }
        private Artwork ViewModelToModel(ArtworkViewModel viewModel)
        {
            string uniqueFileName = UploadedFile(viewModel);
            return new Artwork()
            {
                ProductId = viewModel.ArtId,
                Title = viewModel.Title,
                Type = viewModel.Type,
                Length = viewModel.Length,
                Width = viewModel.Width,
                Price = viewModel.Price,
                ArtImage = uniqueFileName,
                Materials = viewModel.Materials.ToModel(m => new Material
                {
                    MaterialId = m.MaterialId,
                    Title = m.Title,
                    Category = m.Category
                }).ToList()
            };
        }

        // GET: Artworks
        public async Task<IActionResult> Artwork()
        {
            return View(await _context.Artwork.ToListAsync());
        }

        // GET: Artworks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artwork
                .Include(x => x.Materials)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (artwork == null)
            {
                return NotFound();
            }

            return View(ModelToViewModel(artwork));
        }

        // GET: Artworks/Create
        public IActionResult Create()
        {
            return View(new ArtworkViewModel());
        }

        // POST: Artworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArtworkViewModel artwork)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(artwork);

                Artwork  art = new Artwork()
                {
                    Title = artwork.Title,
                    Type = artwork.Type,
                    Length = artwork.Length,
                    Width = artwork.Width,
                    Price = artwork.Price,
                    ArtImage = uniqueFileName,
                    Materials = artwork.Materials.ToModel(m => new Material
                    {
                        MaterialId = m.MaterialId,
                        Title = m.Title,
                        Category = m.Category
                    }).ToList()
                };

                _context.Add(art);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Artwork));
            }
            return View();
        }

        // GET: Artworks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artwork
                .Include(x => x.Materials)
                .FirstOrDefaultAsync(x => x.ProductId == id);
            var artworkViewModel = new ArtworkViewModel()
            {
                ArtId = artwork.ProductId,
                Title = artwork.Title,
                Type = artwork.Type,
                Length = artwork.Length,
                Width = artwork.Width,
                Price = artwork.Price,
                ExistingImage = artwork.ArtImage,
                Materials = artwork.Materials.ToDynamicList(m => new MaterialViewModel()
                {
                    MaterialId = m.MaterialId,
                    Title = m.Title,
                    Category = m.Category
                })
            };
            if (artwork == null)
            {
                return NotFound();
            }
            return View(artworkViewModel);
        }

        // POST: Artworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArtworkViewModel artwork)
        {
            if (id != artwork.ArtId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(artwork);
                var artModel = new Artwork();

                artModel.ProductId = artwork.ArtId;
                artModel.Title = artwork.Title;
                artModel.Type = artwork.Type;
                artModel.Length = artwork.Length;
                artModel.Width = artwork.Width;
                artModel.Price = artwork.Price;
                artModel.ArtImage = uniqueFileName;
                artModel.Materials = artwork.Materials.ToModel(m => new Material
                {
                    MaterialId = m.MaterialId,
                    Title = m.Title,
                    Category = m.Category
                }).ToList();
                if (artwork.ProductImage == null)
                {
                    artModel.ArtImage = uniqueFileName;
                }
                else if (artwork.ProductImage != null)
                {
                    if (artwork.ExistingImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", artwork.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }
                    artModel.ArtImage = UploadedFile(artwork);
                }
                _context.Update(artModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Artwork));
            }
            return View(artwork);
        }

        // GET: Artworks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artwork
                .Include(x => x.Materials)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (artwork == null)
            {
                return NotFound();
            }

            return View(ModelToViewModel(artwork));
        }

        // POST: Artworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artwork = await _context.Artwork
                .Include(x => x.Materials)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            _context.Artwork.Remove(artwork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Artwork));
        }
        private string UploadedFile(ArtworkViewModel artwork)
        {
            string uniqueFileName = null;

            if (artwork.ProductImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + artwork.ProductImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    artwork.ProductImage.CopyTo(fileStream);

                }
            }
            return uniqueFileName;
        }
        private bool ArtworkExists(int id)
        {
            return _context.Artwork.Any(e => e.ProductId == id);
        }
    }
}
