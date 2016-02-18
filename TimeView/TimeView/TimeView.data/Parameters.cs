using System.Collections.Generic;

namespace TimeView.data
{
    public class Parameters
    {
        public IList<string> dataset { get; set; }
        public string timezone { get; set; }
        public int rows { get; set; }
        public string format { get; set; }
    }
}