using System.Windows;
using System.Windows.Controls;

namespace TimeView.wpf
{
    /// <summary>
    ///     Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        // Specific method because the password property cannot bind due to security regulations within .NET
        // This solution does not violate with MVVM pattern
        // http://stackoverflow.com/questions/1483892/how-to-bind-to-a-passwordbox-in-mvvm

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((dynamic) DataContext).Employee.Password = ((PasswordBox) sender).Password;
            }
        }
    }
}