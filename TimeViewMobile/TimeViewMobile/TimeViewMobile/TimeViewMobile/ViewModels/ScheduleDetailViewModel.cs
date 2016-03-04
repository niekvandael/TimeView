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
using Xamarin.Forms;

namespace TimeViewMobile.ViewModels
{
    public class ScheduleDetailViewModel : INotifyPropertyChanged
    {

        private readonly ICategoryEntryDataService _categoryEntryDataService;
        private readonly IScheduleDataService _scheduleDataService;

        private Picker _categoryEntryPicker;

        public ICommand SaveCommand { get; set; }

        public ScheduleDetailViewModel(ICategoryEntryDataService categoryEntryDataService, IScheduleDataService scheduleDataService, Picker categoryEntryPicker)
        {
            // Register to events
            MessagingCenter.Subscribe<DetailMessage, Employee>(this, "LoadScheduleForUser", (sender, arg) => {
                this.SelectedEmployee = arg;
                LoadData();
            });

            MessagingCenter.Subscribe<LoadDetailMessage>(this, "LoadScheduleDetailView", (sender) => {
                if (sender.IsScheduleDetail)
                {
                    this.Message = "";
                    this.Schedule = sender.Schedule;

                    SetItemInCategoryEntryPicker();
                }
            });

            // Set data
            this._categoryEntryDataService = categoryEntryDataService;
            this._categoryEntryPicker = categoryEntryPicker;
            this._scheduleDataService = scheduleDataService;

            // Set Commands
            SaveCommand = new Command(SaveAction);
        }
        

        // Binding to picker not yet available in Xamarin
        private void SetItemInCategoryEntryPicker() {
            if (_schedule.CategoryEntry == null)
            {
                return; // New Instance
            }
            for (int i = 0; i < _categoryEntryPicker.Items.Count; i++)
            {
                if (_categoryEntryPicker.Items[i].Equals(_schedule.CategoryEntry.Name)) {
                    _categoryEntryPicker.SelectedIndex = i;
                }
            }
        }

        // Binding to picker not yet available in Xamarin
        private void GetItemFromCategoryEntryPicker() {
            var selection = _categoryEntryPicker.Items[_categoryEntryPicker.SelectedIndex];

            foreach (CategoryEntry categoryEntry in CategoryEntries)
            {
                if (categoryEntry.Name.Equals(selection)) {
                    Schedule.CategoryEntryId = categoryEntry.Id;
                }
            }

        }

        private void SaveAction() {
            List<Schedule> SchedulesToUpdate = new List<Schedule>();
            // No binding on CategoryEntryPicker: it is not supported
            GetItemFromCategoryEntryPicker();
            SchedulesToUpdate.Add(Schedule);

            this._scheduleDataService.SaveSchedules(SchedulesToUpdate, SaveCallback);
        }

        private bool SaveCallback(bool success)
        {
            if (success)
            {
                MessagingCenter.Send(new LoadScheduleList(), "LoadScheduleList");
                MessagingCenter.Send<LoadDetailMessage>(new LoadDetailMessage { IsScheduleList = true }, "LoadScheduleDetailView");
            }
            else {
                Message = "Error occured";
            }
            return true;
        }

        private async void LoadData() {
            var result = await _categoryEntryDataService.GetCategoryEntriesForCompany(SelectedEmployee.CompanyId); // TODO
            this.CategoryEntries = result.ToObservableCollection();

            // Items is not yet available in xaml
            // https://developer.xamarin.com/api/type/Xamarin.Forms.Picker/
            while (_categoryEntryPicker.Items.Count != 0)
            {
                _categoryEntryPicker.Items.RemoveAt(0);
            }

            foreach (var item in CategoryEntries)
            {
                _categoryEntryPicker.Items.Add(item.Name);  
            }
        }

        private ObservableCollection<CategoryEntry> _categoryEntries;
        public ObservableCollection<CategoryEntry> CategoryEntries
        {
            get { return _categoryEntries; }
            set
            {
                _categoryEntries = value;
                RaisePropertyChanged("CategoryEntries");
            }
        }

        private ObservableCollection<String> _categoryEntriesPicker;
        public ObservableCollection<String> CategoryEntriesPicker
        {
            get { return _categoryEntriesPicker; }
            set
            {
                _categoryEntriesPicker = value;
                RaisePropertyChanged("CategoryEntriesPicker");
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

        private Schedule _schedule;
        public Schedule Schedule
        {
            get { return _schedule; }
            set
            {
                _schedule = value;
                RaisePropertyChanged("Schedule");
            }
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
