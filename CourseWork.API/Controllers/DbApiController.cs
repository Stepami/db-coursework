using CourseWork.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork.API.Controllers
{
    /// <summary>
    /// Контроллер с доступом к бд
    /// </summary>
    public class DbApiController : ApiController
    {
        protected readonly CWContext db;

        public DbApiController(CWContext db) => this.db = db;
    }
}
