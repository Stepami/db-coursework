using CourseWork.Lib;
using CourseWork.Lib.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork.API.Controllers
{
    public class TrajectoriesController : DbApiController
    {
        public TrajectoriesController(CWContext db) : base(db) { }

        [Authorize]
        [HttpPut]
        [Route("{id}/pass")]
        public async Task<TrajectoryElement> PassTrajectoryStep(Guid id)
        {
            var trajectory = await db.Trajectories.FindAsync(id);
            var step = trajectory.Pass();
            if (step != null)
            {
                db.Update(step);
                await db.SaveChangesAsync();
            }
            return step;
        }
    }
}
