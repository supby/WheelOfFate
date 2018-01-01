using System;
using System.Collections.Generic;
using System.Text;

namespace WheelOfFate.Models.DTO
{
    public class HistoryRecordDTO
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime Start { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
