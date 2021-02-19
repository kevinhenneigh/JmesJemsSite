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

namespace JmesJemsSite.Controllers
{
    public class ArtworksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtworksController(ApplicationDbContext context)
        {
            _context = context;

            if (_context.Artwork.Count() == 0)
            {
                _context.Artwork.Add(new Artwork
                {
                    Title = "Goo Guy",
                    Type = "Abstract",
                    Length = 12.5,
                    Width = 12.5,
                    Price = 25.99,
                    Materials = {
                        new Material { Title = "Canvas", Category = "Plant-Based" },
                        new Material{ Title = "Oil-based", Category = "Paint"} }
                });
                _context.Artwork.Add(new Artwork
                {
                    Title = "New new",
                    Type = "Impressionism",
                    Length = 24.00,
                    Width = 15.00,
                    Price = 50.99,
                    Materials = {
                        new Material { Title = "Canvas", Category = "Plant-Based" },
                        new Material{ Title = "Pastels", Category = "Soft"}}
                });
                _context.SaveChanges();
            }
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
            var newArtMaterialViewModel = new ArtMaterialViewModel()
            {
                Title = "New Material"
            };

            // Now you have to call the extension PartialView method passing the view model.
            // This is an extension method that creates a partial view with the needed HTML 
            // prefix for the fields in your form so the form will post correctly when it 
            // gets submitted.
            return this.PartialView(newArtMaterialViewModel, parameters);
        }
        private static ArtworkViewModel ModelToViewModel(Artwork model)
        {
            var artworkViewModel = new ArtworkViewModel()
            {
                ArtId = model.ProductId,
                Title = model.Title,
                Type = model.Type,
                Length = model.Length,
                Width = model.Width,
                Price = model.Price,
                ArtMaterials = model.Materials.ToDynamicList(m => new MaterialViewModel()
                {
                    MaterialId = m.MaterialId,
                    Title = m.Title,
                    Category = m.Category
                })
            };

            return artworkViewModel;
        }
        private static Artwork ViewModelToModel(ArtworkViewModel viewModel)
        {
            return new Artwork()
            {
                ProductId = viewModel.ArtId,
                Title = viewModel.Title,
                Type = viewModel.Type,
                Length = viewModel.Length,
                Width = viewModel.Width,
                Price = viewModel.Price,
                Materials = viewModel.ArtMaterials.ToModel(m => new Material
                {
                    MaterialId = m.MaterialId,
                    Title = m.Title,
                    Category = m.Category
                }).ToList()
            };
        }

        // GET: Artworks
        public async Task<IActionResult> Index()
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
                _context.Add(ViewModelToModel(artwork));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artwork);
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
            if (artwork == null)
            {
                return NotFound();
            }
            return View(ModelToViewModel(artwork));
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
                try
                {
                    _context.Update(ViewModelToModel(artwork));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtworkExists(artwork.ArtId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }

        private bool ArtworkExists(int id)
        {
            return _context.Artwork.Any(e => e.ProductId == id);
        }
    }
}
