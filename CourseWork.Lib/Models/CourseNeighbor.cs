using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Lib.Models
{
    public record CourseNeighbor
    {
        public int ID { get; init; }

        public double Dist { get; init; }
    }
}
