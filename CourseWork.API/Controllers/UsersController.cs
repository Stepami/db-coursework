using CourseWork.API.Generators;
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
        [HttpGet]
        [Route("user/trajectories")]
        public async Task<IEnumerable<Trajectory>> TrajectoriesOfCurrentUser()
        {
            var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await db.Users.FindAsync(id);
            return user.Trajectories;
        }

        [Authorize]
        [Route("user/trajectories/new")]
        [HttpPost]
        public async Task<Trajectory> NewTrajectory(string specId, int size = 10)
        {
            var spec = await db.Specializations.FirstOrDefaultAsync(s => s.ID == specId);
            if (spec != null)
            {
                var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var user = await db.Users.FindAsync(id);

                user.Generator = new RestNlpGenerator(db);
                var trajectory = user.NewTrajectory(spec, size);

                db.Trajectories.Add(trajectory);
                await db.SaveChangesAsync();

                return trajectory;
            }
            return null;
        }
    }
}
