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
        /// Генератор траектории
        /// </summary>
        [NotMapped]
        public ITrajectoryGenerator Generator { private get; set; }

        /// <summary>
        /// Создание новой траектории
        /// </summary>
        /// <param name="specialization">Требуемая специализация после прохождения траектории</param>
        /// <param name="size">Размер траектории (кол-во этапов)</param>
        /// <returns>Новая траектория</returns>
        public Trajectory NewTrajectory(Specialization specialization, int size) => Generator.Generate(specialization, ID, size);

        protected override Guid DefaultID() => Guid.NewGuid();
    }
}
