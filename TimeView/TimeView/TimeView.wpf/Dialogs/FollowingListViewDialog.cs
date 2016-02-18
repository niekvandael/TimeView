using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeView.wpf.Dialogs;
using TimeView.wpf.View;

namespace TimeView.wpf.Services
{
    public class FollowingListViewDialog : IViewDialog
    {
        Window followingListView = null;

        public void ShowDialog(String Title) {
            if (this.followingListView == null) {
                followingListView = new FollowingListView();
                followingListView.Title = Title;
            }
            followingListView.Show();
        }

        public void CloseDialog()
        {
            if (this.followingListView != null)
            {
                followingListView.Close();
            }
        }
    }
}
