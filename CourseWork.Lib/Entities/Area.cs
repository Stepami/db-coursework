using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Lib.Entities
{
    /// <summary>
    /// Тип области
    /// </summary>
    public enum AreaType : byte
    {
        /// <summary>
        /// Профессиональная
        /// </summary>
        Professional,

        /// <summary>
        /// Отрасль
        /// </summary>
        Industry
    }

    /// <summary>
    /// Область
    /// </summary>
    [Table("Areas")]
    public class Area : Entity<string>
    {
        /// <summary>
        /// Название области
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Тип области
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AreaType Type { get; set; }

        /// <summary>
        /// Специализации в этой области
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Specialization> Specializations { get; set; } = new List<Specialization>();

        protected override string DefaultID() => Guid.NewGuid().ToString();
    }
}
