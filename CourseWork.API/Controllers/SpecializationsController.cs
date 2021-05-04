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
    public class SpecializationsController : EntitiesController<Specialization, string>
    {
        public SpecializationsController(CWContext db) : base(db) { }

        [HttpGet]
        [Route("laboring")]
        public async Task<IEnumerable<Specialization>> Laboring() => await db.Specializations.AsNoTracking().Where(s => s.Laboring).ToListAsync();
    }
}
