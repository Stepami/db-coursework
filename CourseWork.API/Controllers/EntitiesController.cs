using CourseWork.Lib;
using CourseWork.Lib.Entities;
using CourseWork.Lib.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork.API.Controllers
{
    public class EntitiesController<T, IdType> : DbApiController where T : Entity<IdType>, new()
    {
        public EntitiesController(CWContext db) : base(db) { }

        [HttpGet]
        [Route("{id}")]
        public async Task<T> Get(IdType id) => await db.Set<T>().FirstOrDefaultAsync(e => e.ID.Equals(id));

        [HttpGet]
        [Route("paged/{page}")]
        public async Task<PagedList<T>> GetPagedAsync(int page) => await PagedList<T>.CreateAsync(db.Set<T>(), page);
    }
}
