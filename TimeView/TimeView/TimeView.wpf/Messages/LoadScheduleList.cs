using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.wpf.Messages
{
    public class LoadScheduleList
    {
        public Employee Employee { get; set; }
        public bool MySchedule { get; set; }
    }
}
