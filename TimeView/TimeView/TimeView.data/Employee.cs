using System;
using System.Collections.Generic;

namespace TimeView.data
{
    public class Employee
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
