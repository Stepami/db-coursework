using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWork.API.Controllers
{
    /// <summary>
    /// Базовый класс для контроллеров, реализующих методы API
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/")]
    public class ApiController : ControllerBase { }
}
