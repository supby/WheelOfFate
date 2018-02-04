using System;
using System.Collections.Generic;
using System.Text;
using WheelOfFate.Models.DTO;

namespace WheelOfFate.Interfaces.Services
{
    /// <summary>
    /// Service is responsible for all BAU related operation
    /// </summary>
    public interface IBAUService
    {
        /// <summary>
        /// Return random employees for BAU according params
        /// </summary>
        /// <param name="bauCapacity">How many employee</param>
        /// <param name="minShift">Minimal distance for employee between shifts </param>
        /// <param name="workingWindow">Window in days which considered as working cycle</param>
        /// <param name="reqDaysPerWindow">How many days eployee should have per working cycle</param>
        /// <returns></returns>
        IEnumerable<EmployeeDTO> GetFor(int bauCapacity, int minShift, int workingWindow, int reqDaysPerWindow);

        /// <summary>
        /// Add Employees to shift
        /// </summary>
        /// <param name="employeeIds">Ids of employees to add</param>
        /// <param name="shiftDurationHours">Shift duration in hours</param>
        void AddShift(IEnumerable<int> employeeIds, int shiftDurationHours);

        /// <summary>
        /// Return list of employees in current shift
        /// </summary>
        /// <returns>List of EmployeeInShiftDTO</returns>
        IEnumerable<EmployeeInShiftDTO> GetCurrentShift();
    }
}
