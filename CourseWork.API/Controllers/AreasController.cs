using CourseWork.Lib;
using CourseWork.Lib.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork.API.Controllers
{
    public class AreasController : EntitiesController<Area, string>
    {
        public AreasController(CWContext db) : base(db) { }

        [HttpGet]
        [Route("{areaID}/specializations")]
        public async Task<IEnumerable<Specialization>> GetSpecializationsAsync(string areaID) 
        {
            var area = await db.Areas.FirstOrDefaultAsync(a => a.ID == areaID);

            if (area != null)
            {
                return area.Specializations;
            }

            return new List<Specialization>();
        }
    }
}
