using System.Collections.Generic;

namespace TimeView.data
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public virtual List<Employee> Employees { get; set; }
    }
}