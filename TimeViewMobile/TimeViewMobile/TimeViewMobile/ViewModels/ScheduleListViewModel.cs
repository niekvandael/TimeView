using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.wpf.Services;
using TimeViewMobile.Extensions;
using TimeViewMobile.Messages;
using Xamarin.Forms;

namespace TimeViewMobile.ViewModels
{
    public class ScheduleListViewModel : INotifyPropertyChanged
    {
        private readonly IScheduleDataService _scheduleDataService;
        private readonly IEmployeeDataService _employeeDataService;

        public ScheduleListViewModel(IScheduleDataService scheduleDataService, IEmployeeDataService employeeDataService)
        {
            // Register to events
            MessagingCenter.Subscribe<DetailMessage, Employee>(this, "LoadScheduleForUser", (sender, arg) => {
                this.SelectedEmployee = arg;
                LoadData();
            });

            // Dialogs

            // set services
            _scheduleDataService = scheduleDataService;
            _employeeDataService = employeeDataService;

            // Load data & commands
            this.Schedules = new ObservableCollection<Schedule>();
        }

        private String _title = "Schedule for ...";
        public String Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        private ObservableCollection<Schedule> _schedules = new ObservableCollection<Schedule>();
        public ObservableCollection<Schedule> Schedules
        {
            get { return _schedules; }
            set
            {
                _schedules = value;
                RaisePropertyChanged("Schedules");
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                RaisePropertyChanged("SelectedEmployee");
            }
        }

      
        public event PropertyChangedEventHandler PropertyChanged;

        public async void LoadData()
        {
            // Hardcoded userid because of lack of time to implement settings screen
            SelectedEmployee = await _employeeDataService.GetEmployee(_selectedEmployee);
            var schedules = await _scheduleDataService.GetScheduleForEmployee(SelectedEmployee);
            Schedules = schedules.ToObservableCollection();

            foreach (Schedule schedule in Schedules)
            {
                schedule.CategoryEntry.Name = char.ToUpper(schedule.CategoryEntry.Name[0]) + schedule.CategoryEntry.Name.Substring(1).ToString();
            }
        }


        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
