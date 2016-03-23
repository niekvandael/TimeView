using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.DAL.Repositories.Employee;
using TimeView.DAL.Repositories.Schedule;
using TimeView.wpf.Services;
using TimeViewMobile.ViewModels;
using Xamarin.Forms;

namespace TimeViewMobile.Views
{
    public partial class AddFollower : ContentPage
    {
        public AddFollower()
        {
            InitializeComponent();


            //
            // Repositories
            //
            IEmployeeDataService EmployeeDataService = new EmployeeDataService(new EmployeeRepository());

            //
            // ListViews
            
            AddFollowerViewModel AddFollowerViewModel = new AddFollowerViewModel(EmployeeDataService);

            this.BindingContext = AddFollowerViewModel;
        }
    }
}
