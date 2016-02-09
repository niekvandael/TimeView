using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.context
{
    public class TimeViewContext : DbContext
    {
        public DbSet<Company> Company { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryEntry> CategoryEntry { get; set; }

        public TimeViewContext()
            : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}
