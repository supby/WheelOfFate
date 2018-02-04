using System;
using System.Collections.Generic;
using System.Text;
using WheelOfFate.Models.DTO;

namespace WheelOfFate.Interfaces.Services
{
    /// <summary>
    /// Simple employees management service
    /// </summary>
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDTO> Save(IEnumerable<EmployeeDTO> employees);

        void Delete(int id);

        IEnumerable<EmployeeDTO> Get();
    }
}
