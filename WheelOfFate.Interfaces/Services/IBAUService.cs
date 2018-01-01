using System;
using System.Collections.Generic;
using System.Text;
using WheelOfFate.Models.DTO;

namespace WheelOfFate.Interfaces.Services
{
    public interface IBAUService
    {
        IEnumerable<EmployeeDTO> GetFor(int bauCapacity, int minShift, int workingWindow, int reqDaysPerWindow);
    }
}
