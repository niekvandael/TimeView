using System;

namespace TimeView.data
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Employee Employee { get; set; }
    }
}