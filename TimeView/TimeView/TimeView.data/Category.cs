using System.Collections.Generic;

namespace TimeView.data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CategoryEntry> CategorieEntries { get; set; }
    }
}