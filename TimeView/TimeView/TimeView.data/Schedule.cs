using System;

namespace TimeView.data
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime Day { get; set; }

        public int CategoryEntryId { get; set; }
        public CategoryEntry CategoryEntry { get; set; }

        public int EmployeeId { get; set; }
    }
}