using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        private ObservableCollection<CategoryEntry> _categoryEntries;
        private readonly ICategoryEntryDataService _categoryEntryDataService;
        private Employee _employee;
        private string _message;
        private string _messageColor;
        private bool _mySchedule;
        private readonly IScheduleDataService _scheduleDataService;
        private ObservableCollection<Schedule> _schedules;
        private Schedule _selectedSchedule;

        public ScheduleListViewModel(IScheduleDataService scheduleDataService,
            ICategoryEntryDataService categoryEntryDataService)
        {
            // Register to events
            Messenger.Default.Register<LoadScheduleList>(this, OnLoadScheduleReceived);

            // set services
            _scheduleDataService = scheduleDataService;
            _categoryEntryDataService = categoryEntryDataService;

            // Load data & commands
            LoadCommands();
        }

        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }

        public ObservableCollection<CategoryEntry> CategoryEntries
        {
            get { return _categoryEntries; }
            set
            {
                _categoryEntries = value;
                RaisePropertyChanged("CategoryEntries");
            }
        }

        public ObservableCollection<Schedule> Schedules
        {
            get { return _schedules; }
            set
            {
                _schedules = value;
                RaisePropertyChanged("Schedules");
            }
        }

        public Schedule SelectedSchedule
        {
            get { return _selectedSchedule; }
            set
            {
                _selectedSchedule = value;
                RaisePropertyChanged("SelectedSchedule");
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        public string MessageColor
        {
            get { return _messageColor; }
            set
            {
                _messageColor = value;
                RaisePropertyChanged("MessageColor");
            }
        }

        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                RaisePropertyChanged("Employee");
            }
        }

        public bool MySchedule
        {
            get { return _mySchedule; }
            set
            {
                _mySchedule = value;
                RaisePropertyChanged("MySchedule");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnLoadScheduleReceived(LoadScheduleList loadMySheduleList)
        {
            MySchedule = loadMySheduleList.MySchedule;

            Employee = loadMySheduleList.Employee;
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
            Schedules.Add(new Schedule {EmployeeId = _employee.Id, Id = -1, Day = GetNextDay()});
        }

        private bool CanNewSchedule(object obj)
        {
            return _mySchedule;
        }

        private void SelectionChanged(object obj)
        {
            foreach (var categoryEntry in _categoryEntries)
            {
                if (_selectedSchedule != null && (_selectedSchedule.CategoryEntryId == categoryEntry.Id))
                {
                    SelectedSchedule.CategoryEntry = categoryEntry;
                }
            }
        }

        private bool CanChangeSelection(object obj)
        {
            return true;
        }

        private void ClearMessage()
        {
            Message = "";
        }

        private void SaveSchedule(object obj)
        {
            ClearMessage();
            _scheduleDataService.SaveSchedules(Schedules.ToList(), SaveCallback);
        }

        private bool SaveCallback(bool success)
        {
            if (success)
            {
                MessageColor = "green";
                Message = "Save successfull";
                LoadScheduleList();
            }
            else
            {
                MessageColor = "red";
                Message = "Save failed";
            }
            return true;
        }

        private bool CanSaveSchedule(object obj)
        {
            return _mySchedule;
        }

        public void LoadData()
        {
            LoadScheduleList();
            LoadCategoryEntries();
        }

        public async void LoadScheduleList()
        {
            var schedules = await _scheduleDataService.GetScheduleForEmployee(Employee);
            Schedules = schedules.ToObservableCollection();
        }

        public async void LoadCategoryEntries()
        {
            var categoryEntries = await _categoryEntryDataService.GetCategoryEntriesForCompany(1);
            CategoryEntries = categoryEntries.ToObservableCollection();
        }

        public DateTime GetNextDay()
        {
            var nextDay = DateTime.Now.AddDays(1);
            if (Schedules != null)
            {
                foreach (var schedule in Schedules)
                {
                    if (schedule.Day.Date >= nextDay.Date)
                    {
                        nextDay = schedule.Day.AddDays(1);
                    }
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