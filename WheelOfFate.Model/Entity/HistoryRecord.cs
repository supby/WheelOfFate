using System;
using System.Collections.Generic;
using System.Text;

namespace WheelOfFate.Models.Entity
{
    /// <summary>
    /// EF entity for History table
    /// </summary>
    public class HistoryRecord
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime Start { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
