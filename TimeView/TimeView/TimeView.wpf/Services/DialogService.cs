using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimeView.wpf.Services
{
    public class DialogService
    {
        Window scheduleListView = null;

        public void showDialog() {
            scheduleListView = new ScheduleListView();
            scheduleListView.Show();
        }

        public void CloseDialog()
        {
            if (this.scheduleListView != null)
            {
                scheduleListView.Close();
            }
        }
    }
}
