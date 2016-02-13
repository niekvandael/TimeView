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

        public Employee Employee = new Employee();
        public string message = "";

        public Login()
        {
            InitializeComponent();

            this.DataContext = this.Employee;
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            MessageTextBlock.Text = "";

            this.Employee.Password = PasswordBox.Password;

            Employee _empl = await EmployeesGateway.login(this.Employee);

            if (_empl != null)
            {
                ScheduleListView window = new ScheduleListView();
                window.Show();
                this.Close();
            } else {
                MessageTextBlock.Text = "Incorrect username or password";
            }
            
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}
