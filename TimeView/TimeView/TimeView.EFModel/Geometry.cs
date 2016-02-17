using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeView.data
{
    public class Geometry
    {
        public string type { get; set; }
        public IList<double> coordinates { get; set; }
    }
}
