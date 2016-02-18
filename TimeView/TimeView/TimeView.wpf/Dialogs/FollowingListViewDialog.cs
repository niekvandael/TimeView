using System.Windows;
using TimeView.wpf.Dialogs;
using TimeView.wpf.View;

namespace TimeView.wpf.Services
{
    public class FollowingListViewDialog : IViewDialog
    {
        private Window _followingListView;

        public void ShowDialog(string title)
        {
            if (_followingListView == null)
            {
                _followingListView = new FollowingListView();
                _followingListView.Title = title;
            }
            _followingListView.Show();
        }

        public void CloseDialog()
        {
            if (_followingListView != null)
            {
                _followingListView.Close();
            }
        }
    }
}