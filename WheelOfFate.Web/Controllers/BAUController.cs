using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WheelOfFate.Interfaces.Services;
using WheelOfFate.Models.DTO;

namespace WheelOfFate.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/BAU")]
    public class BAUController : Controller
    {
        private readonly IBAUService bauService;

        public BAUController(IBAUService bauService)
        {
            this.bauService = bauService;
        }

        // GET: api/BAU/5
        [HttpGet("{capacity}", Name = "Get")]
        public IActionResult Get(int capacity)
        {
            if(capacity == 0)
            {
                return BadRequest();
            }
            return Json(bauService.GetFor(capacity, 1, 14, 1));
        }
    }
}
