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

        public override bool Equals(object obj)
        {
            CategoryEntry test = obj as CategoryEntry;
            if (test == null)
            {
                return false;
            }
            return Id == test.Id &&
                Name == test.Name &&
                Start.Date == test.Start.Date &&
                End.Date == test.End.Date &&
                CategoryId == test.CategoryId;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}