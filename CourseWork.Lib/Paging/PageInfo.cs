using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Lib.Paging
{
    /// <summary>
    /// Информация о странице
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// Номер текущей страницы
        /// Если ноль, то возвращаем все данные целиком
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// Кол-во объектов на странице
        /// </summary>
        public int PageSize { get; set; } //todo: вынести в конфиг

        /// <summary>
        /// Всего объектов
        /// </summary>
        public int TotalItems { get; }

        /// <summary>
        /// Всего страниц
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Параметризированный конструктор
        /// </summary>
        public PageInfo(int pn, int ti)
        {
            (PageNumber, TotalItems) = (pn, ti);
            if (pn == 0 && ti != 0)
            {
                PageSize = ti;
            }
            else
            {
                PageSize = 100;
            }
            TotalPages = (int)Math.Ceiling((decimal)TotalItems / PageSize) < 1 ? 1 : (int)Math.Ceiling((decimal)TotalItems / PageSize);
        }
    }
}
