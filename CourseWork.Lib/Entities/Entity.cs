using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Lib.Entities
{
    /// <summary>
    /// Cущность
    /// </summary>
    /// <typeparam name="IdType">Тип идентификатора</typeparam>
    public abstract class Entity<IdType>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public IdType ID { get; set; }

        /// <summary>
        /// Конструктор для инициализации идентификатора
        /// </summary>
        protected Entity()
        {
            ID = DefaultID();
        }

        /// <summary>
        /// Метод для получения значения идентификатора по умолчанию
        /// </summary>
        protected abstract IdType DefaultID();
    }
}
