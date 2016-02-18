using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeView.wpf.Dialogs;

namespace TimeView.wpf.Services
{
    public class ScheduleListViewDialog : IViewDialog
    {
        Window scheduleListView = null;
        Boolean isOpen = false;

        public void ShowDialog(string Title) {
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
