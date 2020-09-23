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
    public class SubCategoryItemsController : Controller
    {
        private readonly IUnitOfWork _context;

        public SubCategoryItemsController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: AdminPanel/SubCategoryItems
        public async Task<IActionResult> Index()
        {
            if ((await _context.SubCategories.GetAsync()).Count() == 0)
            {
                TempData["SweetAlert"] = JsonConvert.SerializeObject(new SweetAlert()
                {
                    Title = "توجه",
                    Text = "ابتدا یک دسته بندی زیرمنو ایجاد کنید",
                    Icon = SweetAlertIcon.info,
                    ShowCloseButton = false,
                    CancelButtonText = "",
                    ComfirmButtonText = "باشــــه",
                    ShowCancelButton = false
                });

                return RedirectToAction("Index", "SubCategories");
            }

            var applicationDbContext = await _context.SubCategoryItems.GetAsync(includeProperties: "SubCategory");
            return View(applicationDbContext.ToList());
        }

        // GET: AdminPanel/SubCategoryItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategoryItem = (await _context.SubCategoryItems.GetAsync(
                where: n => n.Id == id,
                includeProperties: "SubCategory")).FirstOrDefault();

            if (subCategoryItem == null)
            {
                return NotFound();
            }

            return View(subCategoryItem);
        }

        // GET: AdminPanel/SubCategoryItems/Create
        public async Task<IActionResult> CreateAsync()
        {
            if ((await _context.SubCategories.GetAsync()).Count() == 0)
            {
                TempData["SweetAlert"] = JsonConvert.SerializeObject(new SweetAlert()
                {
                    Title = "توجه",
                    Text = "ابتدا یک دسته بندی زیرمنو ایجاد کنید",
                    Icon = SweetAlertIcon.info,
                    ShowCloseButton = false,
                    CancelButtonText = "",
                    ComfirmButtonText = "باشــــه",
                    ShowCancelButton = false
                });

                return RedirectToAction("Index", "SubCategories");
            }

            var catList = new[] { new { Id = 0, Title = "همه" } }.ToList();

            catList.AddRange((await _context.Categories.GetAsync()).Select(n=>new {Id=n.Id,Title=n.Title }));
            ViewData["CategoryId"] = new SelectList(catList, "Id", "Title");

            ViewData["SubCategoryId"] = new SelectList(await _context.SubCategories.GetAsync(), "Id", "Title");
            ViewData["SubCategoriesJSON"] = JsonConvert.SerializeObject((await _context.SubCategories.GetAsync()).Select(n => new { Id = n.Id, Title = n.Title, CategoryId = n.CategoryId }));

            return View();
        }

        // POST: AdminPanel/SubCategoryItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Title,Icon,SubCategoryId")] SubCategoryItem subCategoryItem)
        {
            if (ModelState.IsValid)
            {
                await _context.SubCategoryItems.InsertAsync(subCategoryItem);
                await _context.Commit();
                return RedirectToAction(nameof(Index));
            }

            var catList = new[] { new { Id = 0, Title = "انتخاب کنید" } }.ToList();

            catList.AddRange((await _context.Categories.GetAsync()).Select(n => new { Id = n.Id, Title = n.Title }));
            ViewData["CategoryId"] = new SelectList(catList, "Id", "Title", (await _context.SubCategories.GetByIDAsync(subCategoryItem.SubCategoryId)).CategoryId);
          
           ViewData["SubCategoryId"] = new SelectList(await _context.SubCategories.GetAsync(), "Id", "Title", subCategoryItem.SubCategoryId);
            ViewData["SubCategoriesJSON"] = JsonConvert.SerializeObject((await _context.SubCategories.GetAsync()).Select(n => new { Id = n.Id, Title = n.Title, CategoryId = n.CategoryId }));

            return View(subCategoryItem);
        }

        // GET: AdminPanel/SubCategoryItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategoryItem = await _context.SubCategoryItems.GetByIDAsync(id);
            if (subCategoryItem == null)
            {
                return NotFound();
            }

            int categoryId= (await _context.SubCategories.GetByIDAsync(subCategoryItem.SubCategoryId)).CategoryId;

            var catList = new[] { new { Id = 0, Title = "همه" } }.ToList();

            catList.AddRange((await _context.Categories.GetAsync()).Select(n => new { Id = n.Id, Title = n.Title }));
            ViewData["CategoryId"] = new SelectList(catList, "Id", "Title",categoryId );
          
            ViewData["SubCategoryId"] = new SelectList(await _context.SubCategories.GetAsync(n=>n.CategoryId==categoryId), "Id", "Title", subCategoryItem.SubCategoryId);
            ViewData["SubCategoriesJSON"] = JsonConvert.SerializeObject((await _context.SubCategories.GetAsync()).Select(n => new { Id = n.Id, Title = n.Title, CategoryId = n.CategoryId }));
            return View(subCategoryItem);
        }

        // POST: AdminPanel/SubCategoryItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Title,Icon,SubCategoryId")] SubCategoryItem subCategoryItem)
        {
            if (id != subCategoryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.SubCategoryItems.UpdateAsync(subCategoryItem);
                    await _context.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.SubCategoryItems.GetByIDAsync(subCategoryItem.Id) == null)
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


            int categoryId = (await _context.SubCategories.GetByIDAsync(subCategoryItem.SubCategoryId)).CategoryId;

            var catList = new[] { new { Id = 0, Title = "همه" } }.ToList();

            catList.AddRange((await _context.Categories.GetAsync()).Select(n => new { Id = n.Id, Title = n.Title }));
            ViewData["CategoryId"] = new SelectList(catList, "Id", "Title", categoryId);

            ViewData["SubCategoryId"] = new SelectList(await _context.SubCategories.GetAsync(n => n.CategoryId == categoryId), "Id", "Title", subCategoryItem.SubCategoryId);
            ViewData["SubCategoriesJSON"] = JsonConvert.SerializeObject((await _context.SubCategories.GetAsync()).Select(n=>new { Id=n.Id,Title=n.Title,CategoryId=n.CategoryId}));


            return View(subCategoryItem);
        }

        // GET: AdminPanel/SubCategoryItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategoryItem = (await _context.SubCategoryItems.GetAsync(
                 where: n => n.Id == id,
                 includeProperties: "SubCategory")).FirstOrDefault();

            if (subCategoryItem == null)
            {
                return NotFound();
            }

            return View(subCategoryItem);
        }

        // POST: AdminPanel/SubCategoryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subCategoryItem = await _context.SubCategoryItems.GetByIDAsync(id);
            await _context.SubCategoryItems.DeleteAsync(subCategoryItem);
            await _context.Commit();
            return RedirectToAction(nameof(Index));
        }

    }
}
