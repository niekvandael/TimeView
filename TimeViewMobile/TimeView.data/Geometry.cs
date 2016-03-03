using System.Collections.Generic;

namespace TimeView.data
{
    public class Geometry
    {
        public string type { get; set; }
        public IList<double> coordinates { get; set; }
    }
}