using Microsoft.EntityFrameworkCore;
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
    /// Пользователь
    /// </summary>
    [Table("Users")]
    public class User : Entity<Guid>
    {
        /// <summary>
        /// Список созданных траекторий
        /// </summary>
        public virtual ICollection<Trajectory> Trajectories { get; set; } = new List<Trajectory>();

        /// <summary>
        /// Логин
        /// </summary>
        [Required]
        public string Login { get; set; }
        
        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Создание новой траектории
        /// </summary>
        /// <param name="specialization">Требуемая специализация после прохождения траектории</param>
        /// <returns>Новая траектория</returns>
        public Trajectory NewTrajectory(Specialization specialization)
        {
            Trajectory trajectory = new() { Specialization = specialization, UserID = ID };

            using (var db = new CWContext())
            {
                for (int i = 0; i < 10; i++)
                {
                    var course = db.Courses.OrderBy(c => Guid.NewGuid()).FirstOrDefault();
                    trajectory.TrajectoryElements.Add(new TrajectoryElement
                    {
                        Course = course,
                        Order = i,
                        TrajectoryID = trajectory.ID
                    });
                }
            }

            return trajectory;
        }

        protected override Guid DefaultID() => Guid.NewGuid();
    }
}
