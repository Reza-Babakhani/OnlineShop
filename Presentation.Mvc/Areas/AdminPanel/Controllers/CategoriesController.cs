using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces;

namespace Presentation.Mvc.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _context;

        public CategoriesController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: AdminPanel/Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.GetAsync());
        }

        // GET: AdminPanel/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .GetByIDAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: AdminPanel/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Title,Icon")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _context.Categories.InsertAsync(category);
                await _context.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: AdminPanel/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.GetByIDAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: AdminPanel/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Title,Icon")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Categories.UpdateAsync(category);
                    await _context.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (await _context.Categories.GetByIDAsync(category.Id) == null)
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
            return View(category);
        }

        // GET: AdminPanel/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .GetByIDAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: AdminPanel/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.GetByIDAsync(id);
            await _context.Categories.DeleteAsync(category);
            await _context.Commit();
            return RedirectToAction(nameof(Index));
        }


    }
}
