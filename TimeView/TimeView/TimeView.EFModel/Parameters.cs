using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
