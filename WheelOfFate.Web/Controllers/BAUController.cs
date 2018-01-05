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

        // GET: api/BAU/5/1/14/1
        [HttpGet("{bauCapacity}/{minShift}/{workingWindow}/{reqDaysPerWindow}", Name = "Get")]
        public IActionResult Get(int bauCapacity, int minShift, int workingWindow, int reqDaysPerWindow)
        {
            if(bauCapacity == 0 || minShift == 0 || workingWindow == 0 || reqDaysPerWindow == 0)
            {
                return BadRequest();
            }

            // IEnumerable<EmployeeDTO> GetFor(int bauCapacity, int minShift, int workingWindow, int reqDaysPerWindow);

            //return Json(bauService.GetFor(capacity, 1, 14, 1));
            return Json(bauService.GetFor(bauCapacity, minShift, workingWindow, reqDaysPerWindow));
        }
    }
}
