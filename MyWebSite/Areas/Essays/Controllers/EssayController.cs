using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebSite.Areas.Essays.Models;
using MyWebSite.Core;
using MyWebSite.Datas;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebSite.Areas.Essays.Controllers
{
    [Area("Essay")]
    public class EssayController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EssayController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 随笔列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(int pageIndex = 1)
        {
            IQueryable<Essay> essayIQ = _context.Essay.AsNoTracking()
                .Select(s => new Essay
                {
                    Id = s.Id,
                    Title = s.Title,
                    Summary = s.Summary,
                    Catalog = s.Catalog,
                    CreateTime = s.CreateTime
                }).OrderByDescending(s => s.CreateTime);

            var result = await PaginatedList<Essay>.CreateAsync(
                essayIQ, pageIndex);

            return new JsonResult(result);
        }

        /// <summary>
        /// 随笔详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var essay = await _context.Essay.AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (essay == null)
            {
                return NotFound();
            }

            return new JsonResult(essay);
        }

        /// <summary>
        /// 新建随笔
        /// </summary>
        /// <param name="essay"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiAuthorize]
        public async Task<IActionResult> Create([Bind("Title,Content,Catalog")] Essay essay)
        {
            if (ModelState.IsValid)
            {
                essay.Summary = essay.Content.StripHTML();
                essay.CreateTime = DateTime.Now;
                essay.UpdateTime = DateTime.Now;

                _context.Add(essay);
                await _context.SaveChangesAsync();
                return new JsonResult(essay.Id);
            }
            throw new ArgumentException();
        }

        /// <summary>
        /// 编辑随笔
        /// </summary>
        /// <param name="essay"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiAuthorize]
        public async Task<IActionResult> Edit([Bind("Id,Title,Content,Catalog")] Essay essay)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    essay.Summary = essay.Content.StripHTML();
                    essay.UpdateTime = DateTime.Now;

                    _context.Entry(essay).State = EntityState.Modified;
                    _context.Entry(essay).Property(x => x.Id).IsModified = false;
                    _context.Entry(essay).Property(x => x.CreateTime).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EssayExists(essay.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return new JsonResult(essay.Id);
            }
            throw new ArgumentException();
        }

        [ApiAuthorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var essay = await _context.Essay
                .SingleOrDefaultAsync(m => m.Id == id);
            if (essay == null)
            {
                return NotFound();
            }

            return View(essay);
        }

        private bool EssayExists(string id)
        {
            return _context.Essay.Any(e => e.Id == id);
        }
    }
}
