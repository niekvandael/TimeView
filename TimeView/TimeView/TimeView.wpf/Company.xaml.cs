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
using TimeView.data.Services;

namespace TimeView.wpf
{
    public partial class Company : Window
    {

        public TimeView.data.Company company = new TimeView.data.Company();

        public ObservableCollection<TimeView.data.Category> Categories = new ObservableCollection<TimeView.data.Category>();

        public Company()
        {
            InitializeComponent();

            this.DataContext = company;
            getData();
        }

        public async void getData()
        {
            this.Categories = new ObservableCollection<TimeView.data.Category>(await CategoriesGateway.getCategories());
            CategoriesCombo.ItemsSource = this.Categories; // TODO - this line should get handeled in XAML
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            this.company.CategoryId = this.company.Category.Id;
            HttpResponseMessage response = await CompaniesGateway.putCompany(this.company);

            if (response.IsSuccessStatusCode)
            {
                Register window = new Register();
                window.Show();
                this.Close();
            }
            else {
                if (response.StatusCode.Equals(HttpStatusCode.Conflict))
                {
                    MessageTextBlock.Text = "Company already exists";
                }
            }
        }
    }
}
