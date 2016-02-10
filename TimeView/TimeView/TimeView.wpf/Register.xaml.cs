using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    public partial class Register : Window
    {
        public Employee Employee = new Employee();

        public ObservableCollection<TimeView.data.Company> Companies = new ObservableCollection<TimeView.data.Company>();

        public Register()
        {
            InitializeComponent();

            this.DataContext = this.Employee;
            getData();
        }

        public async void getData() {
            this.Companies = new ObservableCollection<TimeView.data.Company>(await CompaniesGateway.getCompanies());

            CompaniesCombo.ItemsSource = this.Companies; // TODO - this line should get handeled in XAML
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!PasswordBox1.Password.Equals(PasswordBox2.Password)) {
                MessageTextBlock.Text = "Passwords do not match!";
                return;
            }

            this.Employee.CompanyId = this.Employee.CompanyId; // TODO
            this.Employee.Password = PasswordBox1.Password;

            HttpResponseMessage response = await EmployeesGateway.putEmployee(this.Employee);

            if (response.IsSuccessStatusCode)
            {
                Login window = new Login();
                window.Show();
                this.Close();
            }
            else {
                if (response.StatusCode.Equals(HttpStatusCode.Conflict)){
                    MessageTextBlock.Text = "Username already exists";
                }
            }
        }

        private void AddCompany_Click(object sender, RoutedEventArgs e)
        {
            Company window = new Company();
            window.Show();
            this.Close();
        }
    }
}
