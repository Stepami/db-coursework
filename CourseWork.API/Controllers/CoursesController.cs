using CourseWork.Lib;
using CourseWork.Lib.Entities;
using CourseWork.Lib.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork.API.Controllers
{
    public class CoursesController : EntitiesController<Course, int>
    {
        public CoursesController(CWContext db) : base(db) { }
    }
}
