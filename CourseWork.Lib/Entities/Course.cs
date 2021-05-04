using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Lib.Entities
{
    /// <summary>
    /// Курс
    /// </summary>
    [Table("Courses")]
    public class Course : Entity<int>
    {
        /// <summary>
        /// Заголовок курса
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Рейтинг курса
        /// </summary>
        public float Rating { get; set; }

        /// <summary>
        /// Сколько часов в курсе
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// URL страницы панели курса
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Подробное описание курса на основе HTML
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Детальная информация о стоимости курса
        /// </summary>
        public PriceDetail PriceDetail { get; set; }

        protected override int DefaultID() => 0;
    }
}
