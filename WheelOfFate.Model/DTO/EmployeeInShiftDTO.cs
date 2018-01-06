using System;
using System.Collections.Generic;
using System.Text;

namespace WheelOfFate.Models.DTO
{
    public class EmployeeInShiftDTO : EmployeeDTO
    {
        public TimeSpan TimeLeft { get; set; }
    }
}
