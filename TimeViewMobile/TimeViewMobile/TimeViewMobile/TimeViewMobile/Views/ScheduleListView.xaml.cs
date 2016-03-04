using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.DAL.Repositories.Category;
using TimeView.DAL.Repositories.Employee;
using TimeView.DAL.Repositories.Schedule;
using TimeView.data;
using TimeView.wpf.Services;
using TimeViewMobile.Messages;
using TimeViewMobile.ViewModels;
using Xamarin.Forms;

namespace TimeViewMobile.Views
{
    public partial class ScheduleListView : ContentPage
    {
        public ScheduleListView()
        {
            InitializeComponent();

            //
            // Repositories
            //
            IScheduleDataService ScheduleDataService = new ScheduleDataService(new ScheduleRepository());
            IEmployeeDataService EmployeeDataService = new EmployeeDataService(new EmployeeRepository());


            //
            // ListViews
            //
            ScheduleListViewModel ScheduleListViewModel = new ScheduleListViewModel(ScheduleDataService, EmployeeDataService);

            this.BindingContext = ScheduleListViewModel;

            this.ScheduleList.ItemTapped += ItemTabbed;
        }
        
        // Method not yet available in XAML for Xamarin
        private void ItemTabbed(object sender, ItemTappedEventArgs e)
        {
            MessagingCenter.Send<ScheduleListTabbed>(new ScheduleListTabbed { Schedule = (Schedule)this.ScheduleList.SelectedItem}, "ScheduleListTabbed");
        }
    }
}
