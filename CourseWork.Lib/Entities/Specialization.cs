using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Lib.Entities
{
    /// <summary>
    /// Специальность
    /// </summary>
    [Table("Specializations")]
    public class Specialization : Entity<string>
    {
        /// <summary>
        /// Название специальности
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Относится ли специализация к списку рабочих специальностей
        /// </summary>
        public bool Laboring { get; set; }

        /// <summary>
        /// Идентификатор области
        /// </summary>
        public string AreaID { get; set; }

        protected override string DefaultID() => Guid.NewGuid().ToString();
    }
}
