﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TimeView.data
{
    public class Employee
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public virtual List<Employee> Following { get; set; }
        public virtual List<Employee> Follower { get; set; }
    }
}
