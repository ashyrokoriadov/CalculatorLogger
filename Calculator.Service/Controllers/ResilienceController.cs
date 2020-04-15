using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResilienceController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Ping()
        {
            return Ok("Service online");
        }
    }
}