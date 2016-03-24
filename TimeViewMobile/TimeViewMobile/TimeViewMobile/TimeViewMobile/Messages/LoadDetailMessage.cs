using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeViewMobile.Messages
{
    class LoadDetailMessage
    {
        public bool IsScheduleList { get; set; }
        public bool IsScheduleDetail { get; set; }

        public Schedule Schedule { get; set; }

        public CategoryEntry DefaultCategoryEntry { get; set; }
    }
}
