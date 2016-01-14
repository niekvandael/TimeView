using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.context
{
    class TimeViewContext : DbContext
    {
        public DbSet<Company> Company { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Follower> Follower { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
    }
}
