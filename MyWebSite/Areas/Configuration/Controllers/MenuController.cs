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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MyWebSite.Areas.Configuration.ViewModels;

namespace MyWebSite.Areas.Configuration.Controllers
{
    [Area("Configuration")]
    [Authorize]
    public class MenuController : AppController
    {
        private readonly ApplicationDbContext _context;
        private readonly INavMenuService _NavMenuService;

        public MenuController(ApplicationDbContext context, INavMenuService navMenuService)
        {
            _context = context;
            _NavMenuService = navMenuService;
        }

        /// <summary>
        /// 列表页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(MenuIndexQuery query)
        {
            var menus = _context.Menus.AsNoTracking();
            if (!string.IsNullOrEmpty(query.QName))
            {
                menus= menus.Where(s => s.Name.Contains(query.QName.Trim()));
            }
            if (!string.IsNullOrEmpty(query.QId))
            {
                menus = menus.Where(s => s.Id.Contains(query.QId.Trim()));
            }
            if (!string.IsNullOrEmpty(query.QParentId))
            {
                menus = menus.Where(s => s.ParentId == query.QParentId.Trim());
            }
            if (query.QMenuType != null)
            {
                menus = menus.Where(s => s.MenuType == query.QMenuType);
            }

            UpdateDropDownList();
            return View(new MenuIndexVM { Menus = await menus.ToListAsync(), Query = query });
        }

        /// <summary>
        /// 详情页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 新建空白
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 新建保存
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,ParentId,IndexCode,Url,MenuType,Icon,Remarks")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                if (!MenuExists(menu.Id))
                {
                    _context.Add(menu);
                    await _context.SaveChangesAsync();

                    _NavMenuService.InitOrUpdate();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Id", "菜单编号已存在，请修改菜单编号.");
                }
            }
            UpdateDropDownList(menu);
            return View(menu);
        }

        /// <summary>
        /// 开始编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
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
                    Selected = (menu != null && menuParent.Id == menu.ParentId)
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
