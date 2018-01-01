using System;
using System.Collections.Generic;
using System.Text;
using WheelOfFate.Models.DTO;

namespace WheelOfFate.Interfaces.Services
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDTO> Save(IEnumerable<EmployeeDTO> employees);

        void Delete(int id);

        IEnumerable<EmployeeDTO> Get();
    }
}
