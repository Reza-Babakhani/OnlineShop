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
using Newtonsoft.Json;
using Presentation.Mvc.Models;

namespace Presentation.Mvc.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize]
    public class SubCategoriesController : Controller
    {
        private readonly IUnitOfWork _context;

        public SubCategoriesController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: AdminPanel/SubCategories
        public async Task<IActionResult> Index()
        {
            if((await _context.Categories.GetAsync()).Count() == 0)
            {
                TempData["SweetAlert"] = JsonConvert.SerializeObject(new SweetAlert()
                {
                    Title = "توجه",
                    Text = "ابتدا یک دسته بندی منو ایجاد کنید",
                    Icon = SweetAlertIcon.info,
                    ShowCloseButton = false,
                    CancelButtonText = "",
                    ComfirmButtonText = "باشــــه",
                    ShowCancelButton = false
                });

                return RedirectToAction("Index","Categories");
            }

            var applicationDbContext = await _context.SubCategories.GetAsync(includeProperties: "Category");
            return View(applicationDbContext.ToList());
        }

        // GET: AdminPanel/SubCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = (await _context.SubCategories.GetAsync(
                where: n => n.Id == id,
                includeProperties: "Category")).FirstOrDefault();

            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // GET: AdminPanel/SubCategories/Create
        public async Task<IActionResult> CreateAsync()
        {
            if ((await _context.Categories.GetAsync()).Count() == 0)
            {
                TempData["SweetAlert"] = JsonConvert.SerializeObject(new SweetAlert()
                {
                    Title = "توجه",
                    Text = "ابتدا یک دسته بندی منو ایجاد کنید",
                    Icon = SweetAlertIcon.info,
                    ShowCloseButton = false,
                    CancelButtonText = "",
                    ComfirmButtonText = "باشــــه",
                    ShowCancelButton = false
                });

                return RedirectToAction("Index", "Categories");
            }

            ViewData["CategoryId"] = new SelectList(await _context.Categories.GetAsync(), "Id", "Title");
            return View();
        }

        // POST: AdminPanel/SubCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Title,Icon,CategoryId")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                await _context.SubCategories.InsertAsync(subCategory);
                await _context.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _context.Categories.GetAsync(), "Id", "Title", subCategory.CategoryId);
            return View(subCategory);
        }

        // GET: AdminPanel/SubCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories.GetByIDAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _context.Categories.GetAsync(), "Id", "Title", subCategory.CategoryId);
            return View(subCategory);
        }

        // POST: AdminPanel/SubCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Title,Icon,CategoryId")] SubCategory subCategory)
        {
            if (id != subCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _context.SubCategories.UpdateAsync(subCategory);
                    await _context.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.SubCategories.GetByIDAsync(subCategory.Id)==null)
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
            ViewData["CategoryId"] = new SelectList(await _context.Categories.GetAsync(), "Id", "Title", subCategory.CategoryId);
            return View(subCategory);
        }

        // GET: AdminPanel/SubCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = (await _context.SubCategories.GetAsync(
                 where: n => n.Id == id,
                 includeProperties: "Category")).FirstOrDefault();

            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // POST: AdminPanel/SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subCategory = await _context.SubCategories.GetByIDAsync(id);
           await _context.SubCategories.DeleteAsync(subCategory);
            await _context.Commit();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
