using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeView.data
{
    public class Hospital
    {
        public int nhits { get; set; }
        public Parameters parameters { get; set; }
        public IList<Record> records { get; set; }
    }
}
