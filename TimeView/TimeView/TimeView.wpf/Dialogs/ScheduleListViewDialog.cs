using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimeView.wpf.Services
{
    public class ScheduleListViewDialog
    {
        Window scheduleListView = null;
        Boolean isOpen = false;

        public void showDialog(string Title) {
            if (isOpen) {
                this.CloseDialog();
            }

            scheduleListView = new ScheduleListView();
            scheduleListView.Title = Title;

            scheduleListView.Show();
            isOpen = true;
        }

        public void CloseDialog()
        {
            if (this.scheduleListView != null)
            {
                scheduleListView.Close();
                isOpen = false;
            }
        }
    }
}
