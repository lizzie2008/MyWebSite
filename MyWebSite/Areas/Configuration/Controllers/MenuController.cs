using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebSite.Areas.Configuration.Models;
using MyWebSite.Controllers.Abstract;
using MyWebSite.Datas;
using MyWebSite.Extensions;
using MyWebSite.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyWebSite.Areas.Configuration.Controllers
{
    [Area("Configuration")]
    public class MenuController : AppController
    {
        private readonly ApplicationDbContext _context;
        private readonly INavMenuService _NavMenuService;

        public MenuController(ApplicationDbContext context, INavMenuService navMenuService)
        {
            _context = context;
            _NavMenuService = navMenuService;
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

            UpdateDropDownList(menu);
            return View(menu);
        }

        // GET: Configuration/Menu/Create
        public IActionResult Create()
        {
            var model = new Menu
            {
                Id = "MXX_XX_XX",
                IndexCode = 1,
                Icon = "fa-circle-o"
            };
            UpdateDropDownList();
            return View(model);
        }

        // POST: Configuration/Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,ParentId,IndexCode,Url,MenuType,Icon,Remarks")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await _context.SaveChangesAsync();

                _NavMenuService.InitOrUpdate();
                return RedirectToAction(nameof(Index));
            }
            UpdateDropDownList(menu);
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

            UpdateDropDownList(menu);
            return View(menu);
        }

        // POST: Configuration/Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,ParentId,IndexCode,Url,MenuType,Icon,Remarks")] Menu menu)
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
                _NavMenuService.InitOrUpdate();
                return RedirectToAction(nameof(Index));
            }

            UpdateDropDownList(menu);
            return View(menu);
        }

        // POST: Configuration/Menu/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var menu = await _context.Menus.SingleOrDefaultAsync(m => m.Id == id);
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            _NavMenuService.InitOrUpdate();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(string id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }

        /// <summary>
        /// 初始化下拉选择框
        /// </summary>
        /// <param name="menu"></param>
        private void UpdateDropDownList(Menu menu = null)
        {
            var menusParent = _context.Menus.AsNoTracking().Where(s => s.MenuType == MenuTypes.导航菜单);
            List<SelectListItem> listMenusParent = new List<SelectListItem>();
            foreach (var menuParent in menusParent)
            {
                listMenusParent.Add(new SelectListItem
                {
                    Value = menuParent.Id,
                    Text = menuParent.Id + $"({menuParent.Name})",
                    Selected = (menu != null && menuParent.Id == menu.Id)
                });
            }
            ViewBag.ParentIds = listMenusParent;

            if (menu == null)
            {
                ViewBag.MenuTypes = MenuTypes.导航菜单.GetSelectListByEnum();
            }
            else
            {
                ViewBag.MenuTypes = MenuTypes.导航菜单.GetSelectListByEnum(Convert.ToInt32(menu.MenuType));
            }
        }
    }
}
