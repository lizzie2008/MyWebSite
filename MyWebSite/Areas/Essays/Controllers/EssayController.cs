using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebSite.Areas.Essays.Models;
using MyWebSite.Core;
using MyWebSite.Datas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyWebSite.Extensions;

namespace MyWebSite.Areas.Essays.Controllers
{
    public class EssayCatalogInfo
    {
        public int? EssayCatalogID { get; set; }
        public int CatalogCount { get; set; }
        public string EssayCatalogName { get; set; }
    }

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
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10,
            int? essayCatalogID = null, int? essayArchiveID = null, int? essayTagID = null)
        {
            //条件查询
            IQueryable<Essay> essayIQ = _context.Essays.AsNoTracking();
            //分类查询
            if (essayCatalogID != null)
            {
                essayIQ = essayIQ.Where(s => s.EssayCatalogID == (essayCatalogID == 0 ? null : essayCatalogID));
            }
            //归档查询
            if (essayArchiveID != null)
            {
                essayIQ = essayIQ.Where(s => s.EssayArchiveID == essayArchiveID);
            }
            //标签查询
            if (essayTagID != null)
            {
                essayIQ = essayIQ.Where(s => s.EssayTagAssignments.Any(o => o.EssayTagID == essayTagID));
            }
            //返回需要的随笔列
            essayIQ = essayIQ.Select(s => new Essay
            {
                EssayID = s.EssayID,
                Title = s.Title,
                Summary = s.Summary,
                CreateTime = s.CreateTime
            }).OrderByDescending(s => s.CreateTime);
            var essays = await PaginatedList<Essay>.CreateAsync(essayIQ, pageIndex, pageSize);

            #region 获取额外显示信息

            //分类信息
            var catalogSql = @"SELECT CASE
                                 WHEN T0.ESSAYCATALOGID IS NULL THEN
                                  0
                                 ELSE
                                  T0.ESSAYCATALOGID
                               END AS ESSAYCATALOGID,
                               CASE
                                 WHEN T1.NAME IS NULL THEN
                                  '00.未分类'
                                 ELSE
                                  T1.NAME
                               END AS ESSAYCATALOGNAME, COUNT(1) AS CATALOGCOUNT
                          FROM ESSAYS T0
                          LEFT JOIN ESSAYCATALOGS T1
                            ON T0.ESSAYCATALOGID = T1.ESSAYCATALOGID
                         GROUP BY T0.ESSAYCATALOGID, T1.NAME
                         ORDER BY T1.NAME";
            var catalogInfos = _context.Database.GetDbConnection().Query(catalogSql);

            //归档信息
            var archiveSql = @"SELECT T0.ESSAYARCHIVEID, T1.NAME AS ESSAYARCHIVENAME,
                                      COUNT(1) AS ARCHIVECOUNT
                                 FROM ESSAYS T0
                                 LEFT JOIN ESSAYARCHIVES T1
                                   ON T0.ESSAYARCHIVEID = T1.ESSAYARCHIVEID
                                GROUP BY T0.ESSAYARCHIVEID, T1.NAME
                                ORDER BY T1.NAME DESC";
            var archiveInfos = _context.Database.GetDbConnection().Query(archiveSql);

            //归档信息
            var tagSql = @"SELECT T0.ESSAYTAGID, T2.NAME AS ESSAYTAGNAME, COUNT(1) AS TAGCOUNT
                             FROM ESSAYTAGASSIGNMENTS T0
                            INNER JOIN ESSAYS T1
                               ON T0.ESSAYID = T1.ESSAYID
                            INNER JOIN ESSAYTAGS T2
                               ON T0.ESSAYTAGID = T2.ESSAYTAGID
                            GROUP BY T0.ESSAYTAGID, T2.NAME
                            ORDER BY T2.NAME";
            var tagInfos = _context.Database.GetDbConnection().Query(tagSql);

            #endregion

            return new JsonResult(new
            {
                Essays = essays,
                CatalogInfos = catalogInfos,
                ArchiveInfos = archiveInfos,
                TagInfos = tagInfos
            });
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

            var essay = await _context.Essays.AsNoTracking()
                .Include(s => s.EssayCatalog)
                .Include(s => s.EssayArchive)
                .Include(s => s.EssayTagAssignments)
                    .ThenInclude(s => s.EssayTag)
                .SingleOrDefaultAsync(m => m.EssayID == id);
            if (essay == null)
            {
                return NotFound();
            }

            return essay.ToJsonResult();
        }

        /// <summary>
        /// 随笔分类和标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Create()
        {
            var moreInfo = GetCtrlInfo();
            return new JsonResult(new
            {
                Essay = new Essay(),
                EssayCatalogs = moreInfo.EssayCatalogs,
                EssayTags = moreInfo.EssayTags,
            });
        }
        /// <summary>
        /// 新建随笔(保存)
        /// </summary>
        /// <param name="essay"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiAuthorize]
        public IActionResult Create(Essay essay, int[] selectedTags)
        {
            if (selectedTags != null)
            {
                essay.EssayTagAssignments = new List<EssayTagAssignment>();
                foreach (var tag in selectedTags)
                {
                    var tagToAdd = new EssayTagAssignment { EssayID = essay.EssayID, EssayTagID = tag };
                    essay.EssayTagAssignments.Add(tagToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                essay.Summary = essay.Content.StripHTML();
                essay.CreateTime = DateTime.Now;
                essay.UpdateTime = DateTime.Now;

                //归档处理：如果有改月的归档，赋值归档ID，否则，生成新的归档
                var archiveStr = essay.CreateTime.ToString("yyyy年MM月");
                var archiveFind = _context.EssayArchives.AsNoTracking().SingleOrDefault(s => s.Name == archiveStr);
                if (archiveFind == null)
                {
                    var archiveToAdd = new EssayArchive { Name = archiveStr };
                    _context.EssayArchives.Add(archiveToAdd);
                    essay.EssayArchiveID = archiveToAdd.EssayArchiveID;
                }
                else
                {
                    essay.EssayArchiveID = archiveFind.EssayArchiveID;
                }

                _context.Add(essay);
                _context.SaveChanges();
                return new JsonResult(essay.EssayID);
            }
            throw new ArgumentException();
        }

        /// <summary>
        /// 编辑随笔
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var essay = await _context.Essays.AsNoTracking()
                .Include(s => s.EssayCatalog)
                .Include(s => s.EssayArchive)
                .Include(s => s.EssayTagAssignments)
                .SingleOrDefaultAsync(m => m.EssayID == id);
            if (essay == null)
            {
                return NotFound();
            }

            var moreInfo = GetCtrlInfo(essay.EssayTagAssignments.Select(s => s.EssayTagID).ToArray());
            return new
            {
                Essay = essay,
                EssayCatalogs = moreInfo.EssayCatalogs,
                EssayTags = moreInfo.EssayTags,
            }.ToJsonResult();
        }
        /// <summary>
        /// 编辑随笔(保存)
        /// </summary>
        /// <param name="essay"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiAuthorize]
        public async Task<IActionResult> Edit(Essay essay, int[] selectedTags)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    essay.Summary = essay.Content.StripHTML();
                    essay.UpdateTime = DateTime.Now;

                    _context.Entry(essay).State = EntityState.Modified;
                    _context.Entry(essay).Property(x => x.EssayID).IsModified = false;
                    _context.Entry(essay).Property(x => x.CreateTime).IsModified = false;

                    UpdateEssayTags(selectedTags, essay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EssayExists(essay.EssayID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return new JsonResult(essay.EssayID);
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

            var essay = await _context.Essays
                .SingleOrDefaultAsync(m => m.EssayID == id);
            if (essay == null)
            {
                return NotFound();
            }

            return View(essay);
        }

        private bool EssayExists(string id)
        {
            return _context.Essays.Any(e => e.EssayID == id);
        }

        /// <summary>
        /// 初始化控件数据
        /// </summary>
        /// <returns></returns>
        private dynamic GetCtrlInfo(int[] selectedTags = null)
        {
            return new
            {
                EssayCatalogs = _context.EssayCatalogs
                    .AsNoTracking().OrderBy(s => s.Name)
                    .Select(s => new { EssayCatalogID = s.EssayCatalogID, Name = s.Name }),
                EssayTags = _context.EssayTags
                    .AsNoTracking().OrderBy(s => s.Name)
                    .Select(s => new
                    {
                        EssayTagID = s.EssayTagID,
                        Name = s.Name,
                        Selected = selectedTags != null && selectedTags.Contains(s.EssayTagID) ?
                        true : false
                    })
            };
        }
        /// <summary>
        /// 更新随笔的标签
        /// </summary>
        /// <param name="selectedTags"></param>
        /// <param name="essay"></param>
        private void UpdateEssayTags(int[] selectedTags, Essay essay)
        {
            if (selectedTags == null)
            {
                essay.EssayTagAssignments = new List<EssayTagAssignment>();
                return;
            }
            essay.EssayTagAssignments = essay.EssayTagAssignments ?? new List<EssayTagAssignment>();
            var selectedTagsHS = new HashSet<int>(selectedTags);
            var essayTagsHS = new HashSet<int>(essay.EssayTagAssignments.Select(s => s.EssayTagID));
            foreach (var tag in _context.EssayTags)
            {
                if (selectedTagsHS.Contains(tag.EssayTagID))
                {
                    if (!essayTagsHS.Contains(tag.EssayTagID))
                    {
                        essay.EssayTagAssignments.Add(new EssayTagAssignment { EssayID = essay.EssayID, EssayTagID = tag.EssayTagID });
                    }
                }
                else
                {
                    if (essayTagsHS.Contains(tag.EssayTagID))
                    {
                        var tagToRemove = essay.EssayTagAssignments.SingleOrDefault(s => s.EssayTagID == tag.EssayTagID);
                        _context.Remove(tagToRemove);
                    }
                }
            }
        }
    }
}
