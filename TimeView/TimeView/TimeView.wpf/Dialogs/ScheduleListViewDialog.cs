using System.Windows;
using TimeView.wpf.Dialogs;

namespace TimeView.wpf.Services
{
    public class ScheduleListViewDialog : IViewDialog
    {
        private bool _isOpen;
        private Window _window;

        public void ShowDialog(string title)
        {
            if (_isOpen)
            {
                CloseDialog();
            }

            _window = new ScheduleListView();
            _window.Title = title;

            _window.Show();
            _isOpen = true;
        }

        public void CloseDialog()
        {
            if (_window != null)
            {
                _window.Close();
                _isOpen = false;
            }
        }
    }
}