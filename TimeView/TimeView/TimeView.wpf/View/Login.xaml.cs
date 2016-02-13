using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimeView.data;
using TimeView.data.Services;

namespace TimeView.wpf
{
    /// <summary>
    /// Interaction logic for Login.xaml
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
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Employee.Password = ((PasswordBox)sender).Password; }
        }
    }
}
