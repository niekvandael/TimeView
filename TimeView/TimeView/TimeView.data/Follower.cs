using System;
using System.Collections.Generic;

namespace TimeView.data
{
    public class Follower
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public virtual ICollection<Employee> Following { get; set; }
    }
}
