using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Lib.Entities
{
    /// <summary>
    /// Траектория
    /// </summary>
    [Table("Trajectories")]
    public class Trajectory : Entity<Guid>
    {
        /// <summary>
        /// Ключ пользователя, который составил траекторию
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// Последовательность траектории
        /// </summary>
        public virtual ICollection<TrajectoryElement> TrajectoryElements { get; set; } = new List<TrajectoryElement>();

        /// <summary>
        /// Выходная специализация
        /// </summary>
        public virtual Specialization Specialization { get; set; }

        /// <summary>
        /// Пройти следующий этап траектории
        /// </summary>
        /// <returns>Пройденный этап либо <see langword="null" />, если траектория пройдена</returns>
        public TrajectoryElement Pass()
        {
            // получаем первый непройденный этап
            var element = TrajectoryElements
                .OrderBy(te => te.Order)
                .Where(te => !te.Passed)
                .FirstOrDefault();

            // отметка о прохождении
            if (element != null)
            {
                element.Passed = true;
            }

            return element;
        }

        protected override Guid DefaultID() => Guid.NewGuid();
    }
}
