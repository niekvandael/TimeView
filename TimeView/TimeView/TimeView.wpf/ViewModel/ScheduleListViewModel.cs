using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TimeView.data;
using TimeView.wpf.Extensions;
using TimeView.wpf.Messages;
using TimeView.wpf.Services;
using TimeView.wpf.Utility;

namespace TimeView.wpf.ViewModel
{
    public class ScheduleListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IScheduleDataService scheduleDataService;
        private ICategoryEntryDataService categoryEntryDataService;

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        
        private ObservableCollection<CategoryEntry> categoryEntries;
        public ObservableCollection<CategoryEntry> CategoryEntries
        {
            get
            {
                return this.categoryEntries;
            }
            set
            {
                this.categoryEntries = value;
                RaisePropertyChanged("CategoryEntries");
            }
        }


        private ObservableCollection<Schedule> schedules;
        public ObservableCollection<Schedule> Schedules
        {
            get
            {
                return this.schedules;
            }
            set
            {
                this.schedules = value;
                RaisePropertyChanged("Schedules");
            }
        }

        private Schedule selectedSchedule;
        public Schedule SelectedSchedule
        {
            get
            {
                return selectedSchedule;
            }
            set
            {
                selectedSchedule = value;
                RaisePropertyChanged("SelectedSchedule");
            }
        }

        private String message;
        public String Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                RaisePropertyChanged("Message");
            }
        }

        private String messageColor;
        public String MessageColor
        {
            get
            {
                return messageColor;
            }
            set
            {
                messageColor = value;
                RaisePropertyChanged("MessageColor");
            }
        }

        private Employee employee;
        public Employee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                RaisePropertyChanged("Employee");
            }
        }

        private bool mySchedule;
        public bool MySchedule
        {
            get
            {
                return mySchedule;
            }
            set
            {
                mySchedule = value;
                RaisePropertyChanged("MySchedule");
            }
        }

        public ScheduleListViewModel(IScheduleDataService scheduleDataService, ICategoryEntryDataService categoryEntryDataService)
        {
            // Register to events
            Messenger.Default.Register<LoadScheduleList>(this, OnLoadScheduleReceived);

            // set services
            this.scheduleDataService = scheduleDataService;
            this.categoryEntryDataService = categoryEntryDataService;

            // Load data & commands
            LoadCommands();
        }

        private void OnLoadScheduleReceived(LoadScheduleList loadMySheduleList)
        {
            this.MySchedule = loadMySheduleList.MySchedule;

            this.Employee = loadMySheduleList.Employee;
            LoadData();
        }

        private void LoadCommands()
        {
            NewCommand = new CustomCommand(NewSchedule, CanNewSchedule);
            SaveCommand = new CustomCommand(SaveSchedule, CanSaveSchedule);
            SelectionChangedCommand = new CustomCommand(SelectionChanged, CanChangeSelection);
        }

        private void NewSchedule(object obj)
        {
            this.Schedules.Add(new Schedule { EmployeeId = this.employee.Id, Id = -1, Day = getNextDay() });
        }

        private bool CanNewSchedule(object obj)
        {
            return this.mySchedule;
        }

        private void SelectionChanged(object obj)
        {
            foreach (CategoryEntry categoryEntry in this.categoryEntries)
            {
                if (this.selectedSchedule != null && (this.selectedSchedule.CategoryEntryId == categoryEntry.Id)){
                    this.SelectedSchedule.CategoryEntry = categoryEntry;
                }
            }
        }

        private bool CanChangeSelection(object obj)
        {
            return true;
        }

        private void ClearMessage() {
            this.Message = "";
        }

        private void SaveSchedule(object obj)
        {
            this.ClearMessage();
            scheduleDataService.SaveSchedules(this.Schedules.ToList(), SaveCallback);
        }

        private bool SaveCallback(bool success) {
            if (success)
            {
                this.MessageColor = "green";
                this.Message = "Save successfull";
                this.LoadScheduleList();
            }
            else {
                this.MessageColor = "red";
                this.Message = "Save failed";
            }
            return true;
        }

        private bool CanSaveSchedule(object obj)
        {
            return this.mySchedule;
        }

        private void LoadData()
        {
            this.LoadScheduleList();
            this.LoadCategoryEntries();
        }

        private async void LoadScheduleList() {
            Schedule[] schedules = await scheduleDataService.GetScheduleForEmployee(this.Employee);
            this.Schedules = schedules.ToObservableCollection();
        }

        private async void LoadCategoryEntries() {
            CategoryEntry[] categoryEntries = await categoryEntryDataService.GetCategoryEntriesForCompany(1);
            this.CategoryEntries = categoryEntries.ToObservableCollection();
        }

        private DateTime getNextDay() {
            DateTime nextDay = DateTime.Now.AddDays(1);
            foreach (Schedule schedule in this.Schedules)
            {
                if (schedule.Day >= nextDay) {
                    nextDay = schedule.Day.AddDays(1);
                }
            }

            return nextDay;
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
