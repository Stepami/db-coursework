using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Lib.Entities
{
    /// <summary>
    /// Стоимость
    /// </summary>
    [Owned]
    public class PriceDetail
    {
        /// <summary>
        /// Количество условных единиц
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Идентификатор валюты
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Символ валюты
        /// </summary>
        public string CurrencySymbol { get; set; }

        /// <summary>
        /// Строковое представление стоимости
        /// </summary>
        public string PriceString { get; set; }

        public override string ToString() => PriceString;
    }
}
