using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WheelOfFate.Interfaces.Services;
using WheelOfFate.Models.DTO;

namespace WheelOfFate.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/BAU")]
    public class BAUController : Controller
    {
        private readonly IBAUService bauService;
        private readonly IOptions<AppSettings> appSettings;

        public BAUController(IBAUService bauService, IOptions<AppSettings> appSettings)
        {
            this.bauService = bauService;
            this.appSettings = appSettings;
        }

        // GET: api/BAU/5/1/14/1
        [HttpGet("{bauCapacity}/{minShift}/{workingWindow}/{reqDaysPerWindow}", Name = "Get")]
        public IActionResult Get(int bauCapacity, int minShift, int workingWindow, int reqDaysPerWindow)
        {
            if(bauCapacity == 0 || minShift == 0 || workingWindow == 0 || reqDaysPerWindow == 0)
            {
                return BadRequest();
            }
            
            return Json(bauService.GetFor(bauCapacity, minShift, workingWindow, reqDaysPerWindow));
        }

        // GET: api/BAU
        [HttpGet]
        public IActionResult Get()
        {
            return Json(bauService.GetCurrentShift());
        }

        [HttpPost]
        public IActionResult Post([FromBody]IEnumerable<int> ids)
        {
            if(ids == null || ids.Count() == 0)
            {
                return BadRequest();
            }

            bauService.AddShift(ids, appSettings.Value.ShiftDurationHours);
            return Ok();
        }

        
    }
}
