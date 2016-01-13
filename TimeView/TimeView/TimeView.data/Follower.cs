using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeView.data
{
    class Follower
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public List<Employee> Following { get; set; }
    }
}
