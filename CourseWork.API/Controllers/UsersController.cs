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
    public class UsersController : ApiController
    {
        private readonly CWContext db;

        public UsersController(CWContext db) => this.db = db;

        [HttpPost]
        [Route("api/register")]
        public async Task<User> Register(string login, string password) => await db.RegisterAsync(login, password);

        [HttpPost]
        [Route("api/auth")]
        public async Task<User> Auth(string login, string password) => await db.AuthAsync(login, password);

        [Authorize]
        [Route("api/user/trajectories/new")]
        [HttpPost]
        public async Task<Trajectory> NewTrajectory(string specId)
        {
            var spec = await db.Specializations.FirstOrDefaultAsync(s => s.ID == specId);
            if (spec != null)
            {
                var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var user = await db.Users.FindAsync(id);
                var trajectory = user.NewTrajectory(spec);

                db.Trajectories.Add(trajectory);
                await db.SaveChangesAsync();

                return trajectory;
            }
            return null;
        }
    }
}
