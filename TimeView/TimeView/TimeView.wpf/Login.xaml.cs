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

        public Login()
        {
            InitializeComponent();

            this.DataContext = Employee;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Employee.Password = PasswordBox.Password;

            Employee _empl = await EmployeesGateway.login(this.Employee);

            if (_empl != null)
            {
                ScheduleList scheduleList = new ScheduleList(_empl);
                var host = new Window();
                host.Content = scheduleList;
                this.Close();
                host.Show();
            } else {
                MessageBox.Show("login failed, please try again");
            }
            
        }
    }
}
