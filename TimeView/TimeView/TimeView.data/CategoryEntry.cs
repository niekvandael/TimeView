using System;

namespace TimeView.data
{
    public class CategoryEntry
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}