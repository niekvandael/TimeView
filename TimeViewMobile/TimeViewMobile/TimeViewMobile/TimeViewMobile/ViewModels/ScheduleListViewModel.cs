using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeView.data;
using TimeView.wpf.Services;
using TimeViewMobile.Extensions;
using TimeViewMobile.Messages;
using TimeViewMobile.Views;
using Xamarin.Forms;

namespace TimeViewMobile.ViewModels
{
    public class ScheduleListViewModel : INotifyPropertyChanged
    {
        private readonly IScheduleDataService _scheduleDataService;
        private readonly IEmployeeDataService _employeeDataService;

        public ICommand NewCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ScheduleListViewModel(IScheduleDataService scheduleDataService, IEmployeeDataService employeeDataService)
        {
            // Register to events
            MessagingCenter.Subscribe<DetailMessage, Employee>(this, "LoadScheduleForUser", (sender, arg) => {
                this.SelectedEmployee = arg;
                LoadData();
                Editable = sender.MySchedule;
            });

            MessagingCenter.Subscribe<LoadScheduleList>(this, "LoadScheduleList", (sender) => {
                this._selectedSchedule = null;
                LoadData();
            });

            MessagingCenter.Subscribe<ScheduleListTabbed>(this, "ScheduleListTabbed", (sender) => {
                this.SelectedSchedule = sender.Schedule;
                if (this.Editable) {
                    this.EditAction();
                }
            });

            // Dialogs

            // set services
            _scheduleDataService = scheduleDataService;
            _employeeDataService = employeeDataService;

            // Load data & commands
            this._schedules = new ObservableCollection<Schedule>();
            this.Schedules = new ObservableCollection<Schedule>();
            LoadCommands();
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

        private bool _editable;
        public bool Editable
        {
            get { return _editable; }
            set
            {
                _editable = value;
                RaisePropertyChanged("Editable");
            }
        }

        private ObservableCollection<Schedule> _schedules;
        public ObservableCollection<Schedule> Schedules
        {
            get { return _schedules; }
            set
            {
                _schedules = value;
                RaisePropertyChanged("Schedules");
            }
        }

        private Schedule _selectedSchedule;
        public Schedule SelectedSchedule
        {
            get { return _selectedSchedule; }
            set
            {
                _selectedSchedule = value;
                RaisePropertyChanged("SelectedSchedule");
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

     
        public async void LoadData()
        {
            Schedules = new ObservableCollection<Schedule>();
            SelectedEmployee = await _employeeDataService.GetEmployee(_selectedEmployee);
            var schedules = await _scheduleDataService.GetScheduleForEmployee(SelectedEmployee);
            Schedules = schedules.ToObservableCollection();
        }

        private void LoadCommands() {
            this.NewCommand = new Command(NewAction, CanNewOrEditAction);
            this.EditCommand = new Command(EditAction);
        }

        private void EditAction()
        {
            // Message to open the detail screen
            MessagingCenter.Send<LoadDetailMessage>(new LoadDetailMessage { IsScheduleDetail = true, Schedule = this.SelectedSchedule }, "LoadScheduleDetailView");
        }

        private void NewAction() {
            // Message to open the detail screen
            MessagingCenter.Send<LoadDetailMessage>(new LoadDetailMessage { IsScheduleDetail = true, Schedule = new Schedule { Day = DateTime.Today, Id = -1, EmployeeId = SelectedEmployee.Id} }, "LoadScheduleDetailView");
        }

        private bool CanNewOrEditAction() {
            return _editable;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
