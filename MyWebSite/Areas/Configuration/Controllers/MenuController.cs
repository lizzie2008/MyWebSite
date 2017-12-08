using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebSite.Datas;
using MyWebSite.Models.Configuration;

namespace MyWebSite.Areas.Configuration.Controllers
{
    [Area("Configuration")]
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Configuration/Menu
        public async Task<IActionResult> Index()
        {
            return View(await _context.Menus.AsNoTracking().ToListAsync());
        }

        // GET: Configuration/Menu/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Configuration/Menu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Configuration/Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParentId,Name,IndexCode,AreaUrl,ControllerUrl,ActionUrl,MenuType,Icon,Remarks")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menu);
        }

        // GET: Configuration/Menu/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.SingleOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Configuration/Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ParentId,Name,IndexCode,AreaUrl,ControllerUrl,ActionUrl,MenuType,Icon,Remarks")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
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
            return View(menu);
        }

        // GET: Configuration/Menu/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Configuration/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var menu = await _context.Menus.SingleOrDefaultAsync(m => m.Id == id);
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(string id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}
