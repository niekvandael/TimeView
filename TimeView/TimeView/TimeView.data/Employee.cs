using System;
using System.Collections.Generic;

namespace TimeView.data
{
    public class Employee
    {
        public int Id { get; set; }
        public int Name { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public List<Schedule> Schedules { get; set; }
    }
}
