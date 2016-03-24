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

        public AddCategoryEntryViewModel(IEmployeeDataService IEmployeeDataService, ICategoryEntryDataService ICategoryEntryDataService, Picker categoryEntryPicker)
        {
            this._employeeDataService = IEmployeeDataService;
            this._categoryEntryDataService = ICategoryEntryDataService;
            this._categoryEntryPicker = categoryEntryPicker;

            LoadCategoryEntryPicker();
            this._categoryEntry = new CategoryEntry ();

            // Register to events
            MessagingCenter.Subscribe<ShowAddCategoryEntry, Employee>(this, "ShowAddCategoryEntry", (sender, arg) =>
            {
                this._currentUser = arg;
            });

            // Commands
            SaveCommand = new Command(SaveCategoryEntry);
            CancelCommand = new Command(ShowScheduleDetailView);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public IList<String> GetAllColors()
        {
            /*
                All Colors in Xamarin.Forms.Color
            */
            String[] colors = {
                "Aqua",
                "Black",
                "Blue",
                "Fuchsia",
                "Gray",
                "Green",
                "Lime",
                "Maroon",
                "Navy",
                "Olive",
                "Pink",
                "Purple",
                "Red",
                "Silver",
                "Teal",
                "Transparent",
                "White",
                "Yellow"
            };

            foreach (String color in colors)
            {
                this._categoryEntryPicker.Items.Add(color);
            }
            return colors;
        }

        public void LoadCategoryEntryPicker()
        {
            var allColors = this.GetAllColors();
            foreach (String color in allColors)
            {
                this._categoryEntryPicker.Items.Add(color);
            }
        }

        private void ShowScheduleDetailView()
        {
            this.CleanUp();
            MessagingCenter.Send<LoadDetailMessage>(new LoadDetailMessage { IsScheduleDetail = true }, "LoadScheduleDetailView");
            MessagingCenter.Send<LoadDetailMessage>(new LoadDetailMessage { IsScheduleDetail = true, Schedule = null }, "LoadScheduleDetailView");
        }

        private async void SaveCategoryEntry() {
            if (this.CategoryEntry.Start == null || this.CategoryEntry.End == null || this._categoryEntryPicker.SelectedIndex == -1) {
                Message = "Please complete the form";
                return;
            }

            if (this.CategoryEntry.Start > this.CategoryEntry.End) {
                Message = "Start time must be before End time";
                return;
            }

            this.CategoryEntry.Name = this._categoryEntryPicker.Items[this._categoryEntryPicker.SelectedIndex];
            this._categoryEntry.CategoryId = this._currentUser.Company.CategoryId;
            bool success =  await this._categoryEntryDataService.CreateCategoryEntry(this._categoryEntry);

            if (success)
            {
                ShowScheduleDetailView();
            } else {
                Message = "Something went wrong, please try again";
            }
        }

        private void CleanUp()
        {
            this.Message = "";
            this.CategoryEntry = new CategoryEntry();
        }
    }
}
