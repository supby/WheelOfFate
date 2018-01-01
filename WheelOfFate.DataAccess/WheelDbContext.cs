using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WheelOfFate.Models.Entity;

namespace WheelOfFate.DataAccess
{
    public class WheelDbContext : DbContext
    {
        private static bool _created = false;

        public WheelDbContext(DbContextOptions options) : base(options)
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<HistoryRecord> History { get; set; }
    }
}
