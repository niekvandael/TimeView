using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.DAL.Repositories.Category;
using TimeView.DAL.Repositories.Schedule;
using TimeView.wpf.Services;
using TimeViewMobile.ViewModels;
using Xamarin.Forms;

namespace TimeViewMobile.Views
{
    public partial class ScheduleDetailView : ContentPage
    {
        public ScheduleDetailView()
        {
            InitializeComponent();

            //
            // Repositories
            //
            ICategoryEntryDataService CategoryEntryDataService = new CategoryEntryDataService(new CategoryEntryRepository());
            IScheduleDataService ScheduleDataService = new ScheduleDataService(new ScheduleRepository());

            //
            // ListViews
            //
            ScheduleDetailViewModel ScheduleDetailViewModel = new ScheduleDetailViewModel(CategoryEntryDataService, ScheduleDataService, this.CategoryEntryPicker);

            this.BindingContext = ScheduleDetailViewModel;
        }
    }
}
