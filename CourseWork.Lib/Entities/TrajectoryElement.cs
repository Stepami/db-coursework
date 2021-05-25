using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Lib.Entities
{
    /// <summary>
    /// Элемент траектории
    /// </summary>
    [Table("TrajectoryElements")]
    public class TrajectoryElement : Entity<Guid>
    {
        /// <summary>
        /// Курс
        /// </summary>
        public virtual Course Course { get; set; }

        /// <summary>
        /// Порядок прохождения
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Order is non negative")]
        public int Order { get; set; }

        /// <summary>
        /// Ключ траектории
        /// </summary>
        public Guid TrajectoryID { get; set; }

        /// <summary>
        /// Пройден ли этот этап траектории
        /// </summary>
        public bool Passed { get; set; }

        protected override Guid DefaultID() => Guid.NewGuid();
    }
}
