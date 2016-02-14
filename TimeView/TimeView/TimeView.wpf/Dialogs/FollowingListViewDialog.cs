using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TimeView.wpf.View;

namespace TimeView.wpf.Services
{
    public class FollowingListViewDialog
    {
        Window followingListView = null;

        public void showDialog() {
            followingListView = new FollowingListView();
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
