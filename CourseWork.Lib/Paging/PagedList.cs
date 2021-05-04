using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Lib.Paging
{
    /// <summary>
    /// Пагинированный список
    /// </summary>
    /// <typeparam name="T">Тип элемента списка</typeparam>
    public class PagedList<T> where T : new()
    {
        /// <summary>
        /// Вычисленный срез
        /// </summary>
        public IEnumerable<T> Entities { get; set; }

        /// <summary>
        /// Страница
        /// </summary>
        public PageInfo PageInfo { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        private PagedList(IEnumerable<T> entities, PageInfo pageInfo)
        {
            (Entities, PageInfo) = (entities, pageInfo);
            PageInfo.PageSize = Entities.Count();
        }

        /// <summary>
        /// Отобразить коллекцию из T в U
        /// </summary>
        /// <typeparam name="U">новый тип</typeparam>
        /// <param name="map">функция отображения</param>
        /// <returns></returns>
        public PagedList<U> Map<U>(Func<T, U> map) where U : new() => new(Entities.Select(map), PageInfo);

        /// <summary>
        /// Асинхронное создание объекта из упорядоченных данных
        /// </summary>
        /// <param name="query">запрос сущностей</param>
        /// <param name="page">номер страницы</param>
        /// <returns></returns>
        public static async Task<PagedList<T>> CreateAsync(IOrderedQueryable<T> query, int page)
        {
            var pageInfo = new PageInfo(page, query.Count());
            var pagedQuery = page == 0 ? query : query.Skip((page - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);
            return new PagedList<T>(await pagedQuery.ToListAsync(), pageInfo);
        }

        /// <summary>
        /// Асинхронное создание объекта из неупорядоченных данных
        /// </summary>
        /// <param name="query">запрос сущностей</param>
        /// <param name="page">номер страницы</param>
        /// <returns></returns>
        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> query, int page)
        {
            var pageInfo = new PageInfo(page, query.Count());
            var pagedQuery = page == 0 ? query : query.Skip((page - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);
            return new PagedList<T>(await pagedQuery.ToListAsync(), pageInfo);
        }
    }
}
