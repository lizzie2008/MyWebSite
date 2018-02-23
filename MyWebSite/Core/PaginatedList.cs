using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MyWebSite.Core
{
    /// <summary>
    /// 分页信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedList<T>
    {
        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页数总计
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 记录数总计
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IList<int> PageList { get; set; }
        /// <summary>
        /// 记录明细
        /// </summary>
        public IList<T> Items { get; set; }

        public PaginatedList(List<T> items, int totalCount, int totalPages, int pageIndex, int pageSize, int maxPages)
        {
            Items = items;
            TotalCount = totalCount;
            TotalPages = totalPages;
            PageIndex = pageIndex;
            var dividedCount = maxPages / 2;
            var suposeBegPage = pageIndex - dividedCount;
            var suposeEndPage = suposeBegPage + (maxPages - 1);
            PageList = new List<int>();
            if (suposeBegPage > 0 && suposeEndPage > Math.Min(TotalPages, maxPages))
            {
                for (int i = Math.Min(suposeEndPage, TotalPages) - maxPages + 1; i <= Math.Min(suposeEndPage, TotalPages); i++)
                {
                    PageList.Add(i);
                }
            }
            else
            {
                for (int i = 1; i <= Math.Min(TotalPages, maxPages); i++)
                {
                    PageList.Add(i);
                }
            }
        }
        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }

        }
        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
        /// <summary>
        /// 构造分页信息
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize = 10, int maxPages = 10)
        {
            var count = await source.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            pageIndex = pageIndex > totalPages ? 1 : pageIndex;
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, totalPages, pageIndex, pageSize, maxPages);
        }
    }
}
