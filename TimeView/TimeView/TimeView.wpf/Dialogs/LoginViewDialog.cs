using System.Windows;
using TimeView.wpf.Dialogs;

namespace TimeView.wpf.Services
{
    public class LoginViewDialog : IViewDialog
    {
        private Window _window;

        public void ShowDialog(string Title)
        {
            _window = new Login();
            _window.Title = Title;
        }

        public void CloseDialog()
        {
            Application.Current.MainWindow.Hide();
        }
    }
}