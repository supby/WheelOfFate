using System;
using System.Collections.Generic;
using System.Text;

namespace WheelOfFate.Models.Entity
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<HistoryRecord> HistoryRecords { get; set; }
    }
}
