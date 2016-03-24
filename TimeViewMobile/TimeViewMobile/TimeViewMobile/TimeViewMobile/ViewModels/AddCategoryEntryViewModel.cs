using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeView.data;
using TimeView.wpf.Services;
using TimeViewMobile.Extensions;
using TimeViewMobile.Messages;
using Xamarin.Forms;

namespace TimeViewMobile.ViewModels
{
    public class AddCategoryEntryViewModel : INotifyPropertyChanged
    {

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private Picker _categoryEntryPicker;
        private TimePicker _startTimePicker;
        private TimePicker _endTimePicker;

        private readonly IEmployeeDataService _employeeDataService;
        private readonly ICategoryEntryDataService _categoryEntryDataService;

        private Employee _currentUser;
        public Employee CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }

        private String _message;
        public String Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        private CategoryEntry _categoryEntry;
        public CategoryEntry CategoryEntry
        {
            get { return _categoryEntry; }
            set
            {
                _categoryEntry = value;
                RaisePropertyChanged("CategoryEntry");
            }
        }

        public ICommand LoginCommand { get; set; }

        public AddCategoryEntryViewModel(IEmployeeDataService IEmployeeDataService, ICategoryEntryDataService ICategoryEntryDataService, Picker categoryEntryPicker, TimePicker startTimePicker, TimePicker endTimePicker)
        {
            this._employeeDataService = IEmployeeDataService;
            this._categoryEntryDataService = ICategoryEntryDataService;
            this._categoryEntryPicker = categoryEntryPicker;
            this._startTimePicker = startTimePicker;
            this._endTimePicker = endTimePicker;
            LoadCategoryEntryPicker();
            this._categoryEntry = new CategoryEntry ();

            // Register to events
            MessagingCenter.Subscribe<ShowAddCategoryEntry, Employee>(this, "ShowAddCategoryEntry", (sender, arg) =>
            {
                this._currentUser = arg;
            });

            // Commands
            SaveCommand = new Command(SaveCategoryEntry);
            CancelCommand = new Command(ShowScheduleDetailViewWithoutDefault);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void LoadCategoryEntryPicker()
        {
            var allColors = Utilities.GetColors();
            foreach (String color in allColors)
            {
                this._categoryEntryPicker.Items.Add(color);
            }
        }

        private void ShowScheduleDetailViewWithoutDefault() {
            this.ShowScheduleDetailView(null);
        }

        private void ShowScheduleDetailView(CategoryEntry defaultCategoryEntry)
        {
            this.CleanUp();
            MessagingCenter.Send<LoadDetailMessage>(new LoadDetailMessage { IsScheduleDetail = true, Schedule = null, DefaultCategoryEntry = defaultCategoryEntry }, "LoadScheduleDetailView");
        }

        private async void SaveCategoryEntry() {
            TimeSpan start = this._startTimePicker.Time;
            TimeSpan end = this._endTimePicker.Time;

            this.CategoryEntry.Start = new DateTime(1900, 01, 01, this._startTimePicker.Time.Hours, this._startTimePicker.Time.Minutes, this._startTimePicker.Time.Seconds);
            this.CategoryEntry.End = new DateTime(1900, 01, 01, this._endTimePicker.Time.Hours, this._endTimePicker.Time.Minutes, this._endTimePicker.Time.Seconds);

            if (this.CategoryEntry.Start == null || this.CategoryEntry.End == null || this._categoryEntryPicker.SelectedIndex == -1) {
                Message = "Please complete the form";
                return;
            }

            if (this.CategoryEntry.Start >= this.CategoryEntry.End) {
                Message = "Start time must be before End time";
                return;
            }

            this.CategoryEntry.Name = this._categoryEntryPicker.Items[this._categoryEntryPicker.SelectedIndex];
            this._categoryEntry.CategoryId = this._currentUser.Company.CategoryId;
            bool success =  await this._categoryEntryDataService.CreateCategoryEntry(this._categoryEntry);

            if (success)
            {
                ShowScheduleDetailView(this._categoryEntry);
            } else {
                Message = "Something went wrong, please try again";
            }
        }

        private void CleanUp()
        {
            this.Message = "";
            this.CategoryEntry = new CategoryEntry();

            this._categoryEntryPicker.SelectedIndex = -1;
            this._startTimePicker.Time = new TimeSpan();
            this._endTimePicker.Time = new TimeSpan();
        }
    }
}
