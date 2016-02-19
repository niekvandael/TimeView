using System.Windows;
using TimeView.wpf.Dialogs;
using TimeView.wpf.View;

namespace TimeView.wpf.Services
{
    public class OpenDataListViewDialog : IViewDialog
    {
        private Window _openDataListView;

        public void ShowDialog(string title)
        {
            if (_openDataListView == null)
            {
                _openDataListView = new OpenDataListView();
                _openDataListView.Title = title;
            }
            _openDataListView.Show();
        }

        public void CloseDialog()
        {
            if (_openDataListView != null)
            {
                _openDataListView.Close();
            }
        }
    }
}