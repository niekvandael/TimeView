using System.Collections.Generic;

namespace TimeView.data
{
    public class Employee
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public virtual List<Employee> Following { get; set; }
        public virtual List<Employee> Follower { get; set; }

        public override bool Equals(object obj)
        {
            var test = obj as Employee;
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