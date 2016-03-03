using System.Collections.Generic;

namespace TimeView.data
{
    public class Hospital
    {
        public int nhits { get; set; }
        public Parameters parameters { get; set; }
        public IList<Record> records { get; set; }
    }
}