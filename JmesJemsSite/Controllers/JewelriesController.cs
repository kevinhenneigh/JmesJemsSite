﻿using System;
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
    public class JewelriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JewelriesController(ApplicationDbContext context)
        {
            _context = context;

            if(_context.Jewelry.Count() == 0) 
            {
                _context.Jewelry.Add(new Jewelry
                {
                    Title = "Cool",
                    Type = "Necklace",
                    Color = "Red",
                    Size = "Large",
                    Price = 25.99,
                    Materials = { 
                        new Material { Title = "Yarn", Category = "Plant-Based" },
                        new Material{ Title = "Beads", Category = "Plastic"} }
                });
                _context.Jewelry.Add(new Jewelry
                {
                    Title = "Bad",
                    Type = "Bracelet",
                    Color = "Blue",
                    Size = "Small",
                    Price = 15.99,
                    Materials = {
                        new Material { Title = "Yarn", Category = "Plant-Based" },
                        new Material{ Title = "Rhinestone", Category = "Plastic"},
                        new Material{ Title = "Silver", Category = "Metal"} }
                });
                _context.SaveChangesAsync();
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
                JewelryId = model.JewelryId,
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
                JewelryId = viewModel.JewelryId,
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
        // GET: Jewelries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jewelry.ToListAsync());
        }

        // GET: Jewelries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jewelry = await _context.Jewelry
                .Include(x => x.Materials)
                .FirstOrDefaultAsync(m => m.JewelryId == id);
            if (jewelry == null)
            {
                return NotFound();
            }

            return View(ModelToViewModel(jewelry));
        }

        // GET: Jewelries/Create
        public IActionResult Create()
        {
            return View(new JewelryViewModel());
        }

        // POST: Jewelries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JewelryViewModel jewelry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ViewModelToModel(jewelry));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jewelry);
        }

        // GET: Jewelries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jewelry = await _context.Jewelry
                .Include(x => x.Materials)
                .FirstOrDefaultAsync(x => x.JewelryId == id);
            if (jewelry == null)
            {
                return NotFound();
            }
            return View(ModelToViewModel(jewelry));
        }

        // POST: Jewelries/Edit/5
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
                try
                {
                    _context.Update(ViewModelToModel(jewelry));
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JewelryExists(jewelry.JewelryId))
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
            return View(jewelry);
        }

        // GET: Jewelries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jewelry = await _context.Jewelry
                .Include(x => x.Materials)
                .FirstOrDefaultAsync(m => m.JewelryId == id);
            if (jewelry == null)
            {
                return NotFound();
            }

            return View(ModelToViewModel(jewelry));
        }

        // POST: Jewelries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jewelry = await _context.Jewelry.FindAsync(id);
            _context.Jewelry.Remove(jewelry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JewelryExists(int id)
        {
            return _context.Jewelry.Any(e => e.JewelryId == id);
        }
    }
}