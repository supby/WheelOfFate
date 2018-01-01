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
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService emplyeeService)
        {
            this.employeeService = emplyeeService;
        }

        // GET: api/Employee
        [HttpGet]
        public IEnumerable<EmployeeDTO> Get()
        {
            return employeeService.Get();
        }
        
        // POST: api/Employee
        [HttpPost]
        public IActionResult Post([FromBody]IEnumerable<EmployeeDTO> employees)
        {
            if(employees == null || employees.Count() == 0)
            {
                return BadRequest();
            }

            var res = employeeService.Save(employees);

            return Json(res);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            employeeService.Delete(id);
        }
    }
}
