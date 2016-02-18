using System;
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

        public override bool Equals(object obj)
        {
            Employee test = obj as Employee;
            if (test == null)
            {
                return false;
            }
            return Id == test.Id &&
                Username == test.Username &&
                Password == test.Password &&
                Name == test.Name &&
                CompanyId == test.CompanyId;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
