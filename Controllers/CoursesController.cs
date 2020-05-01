using System.Collections.Generic;
using DemoLogin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoLogin.Controllers
{
    [ApiController]
    [Route("api/v1/courses")]
    public class CourseController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [Authorize]
        public IList<CourseModel> Get()
        {
            return new List<CourseModel> {
                new CourseModel { Tag="1928", Title="Introdução ao Entity Framework Core"}
            };
        }
    }
}
