using System;
using System.Collections.Generic;
using System.Text;

namespace WheelOfFate.Models.DTO
{
    /// <summary>
    /// Employee in shift with Time left
    /// </summary>
    public class EmployeeInShiftDTO : EmployeeDTO
    {
        public TimeSpan TimeLeft { get; set; }
    }
}
