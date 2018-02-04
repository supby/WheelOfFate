using System;
using System.Collections.Generic;
using System.Text;

namespace WheelOfFate.Models.Entity
{
    /// <summary>
    /// EF entity for Employee table
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<HistoryRecord> HistoryRecords { get; set; }
    }
}
