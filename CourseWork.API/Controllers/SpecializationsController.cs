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
    public class SpecializationsController : EntitiesController<Specialization, string>
    {
        public SpecializationsController(CWContext db) : base(db) { }

        /// <summary>
        /// Получение траектории, которую пользователь сгенерировал для получения специальности
        /// </summary>
        /// <param name="specId">Идентификатор выбранной специальности</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("{specId}/trajectory")]
        public async Task<Trajectory> GetTrajectoryBySpec(string specId)
        {
            var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return await db.Trajectories.FirstOrDefaultAsync(t => t.UserID == id && t.Specialization.ID == specId);
        }

        [HttpGet]
        [Route("laboring")]
        public async Task<IEnumerable<Specialization>> Laboring() => await db.Specializations.AsNoTracking().Where(s => s.Laboring).ToListAsync();
    }
}
