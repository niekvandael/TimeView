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
    public class LoginViewDialog : IViewDialog
    {
        Window window = null;

        public void ShowDialog(String Title) {
            window = new Login();
            window.Title = Title;
        }

        public void CloseDialog()
        {
            Application.Current.MainWindow.Hide();
        }
    }
}
