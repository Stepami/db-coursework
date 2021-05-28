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
    public class UsersController : DbApiController
    {
        public UsersController(CWContext db) : base(db) { }

        [HttpPost]
        [Route("register")]
        public async Task<User> Register(string login, string password) => await db.RegisterAsync(login, password);

        [Authorize]
        [HttpPost]
        [Route("auth")]
        public async Task<User> Auth()
        {
            var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return await db.Users.FindAsync(id);
        }

        [Authorize]
        [Route("user/trajectory/{specId}")]
        [HttpGet]
        public async Task<Trajectory> GetTrajectoryBySpec(string specId)
        {
            var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return await db.Trajectories.FirstOrDefaultAsync(t => t.UserID == id && t.Specialization.ID == specId);
        }

        [Authorize]
        [Route("user/trajectories/new")]
        [HttpPost]
        public async Task<Trajectory> NewTrajectory(string specId)
        {
            var spec = await db.Specializations.FirstOrDefaultAsync(s => s.ID == specId);
            if (spec != null)
            {
                var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var user = await db.Users.FindAsync(id);
                var trajectory = user.NewTrajectory(spec);
                foreach (var course in trajectory.TrajectoryElements.Select(te => te.Course))
                {
                    db.Entry(course).State = EntityState.Unchanged;
                }
                db.Trajectories.Add(trajectory);
                await db.SaveChangesAsync();

                return trajectory;
            }
            return null;
        }
    }
}
