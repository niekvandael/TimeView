using System;

namespace TimeView.data
{
    public class CategoryEntry
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}