using CourseWork.Lib;
using CourseWork.Lib.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseWork.API.Controllers
{
    public class TrajectoriesController : DbApiController
    {
        public TrajectoriesController(CWContext db) : base(db) { }

        [Authorize]
        [HttpPut]
        [Route("[controller]/{id}/pass")]
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

        [Authorize]
        [HttpDelete]
        [Route("[controller]/{id}")]
        public async Task DeleteById(Guid id)
        {
            var userID = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var trajectory = await db.Trajectories.FirstOrDefaultAsync(t => t.ID == id && t.UserID == userID);
            if (trajectory != null)
            {
                db.Trajectories.Remove(trajectory);
                await db.SaveChangesAsync();
            }
        }
    }
}
