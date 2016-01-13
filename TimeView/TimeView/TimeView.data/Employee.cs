using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeView.data
{
    class Employee
    {
        public int Id { get; set; }
        public int Name { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public List<Schedule> Schedules { get; set; }
    }
}
