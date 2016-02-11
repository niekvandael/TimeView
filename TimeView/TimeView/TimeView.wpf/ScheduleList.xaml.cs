using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimeView.data;
using TimeView.data.Services;

namespace TimeView.wpf
{
    public partial class ScheduleList : Window
    {
        public ObservableCollection<Schedule> Schedule = new ObservableCollection<Schedule>();
        public Employee Employee = new Employee();

        public ObservableCollection<TimeView.data.CategoryEntry> CategoryEntries = new ObservableCollection<TimeView.data.CategoryEntry>();
        public Schedule CurrentSchedule = new data.Schedule();

        public ScheduleList(Employee employee)
        {
            InitializeComponent();
            this.Employee = employee;
            this.getData();

            this.DataContext = this.Schedule;
        }

        public async Task getData()
        {
            // Get Schedule
            this.Schedule = new ObservableCollection<Schedule>(await ScheduleGateway.getScheduleForEmployee(this.Employee.Id));
            ScheduleListView.ItemsSource = this.Schedule;

            // Get combo entries
            this.CategoryEntries = new ObservableCollection<TimeView.data.CategoryEntry>(await CategoryEntriesGateway.getCategoryEntriesForCategory(this.Employee.Company.CategoryId));
            CategoryEntriesCombo.ItemsSource = this.CategoryEntries; // TODO - this line should get handeled in XAML
        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object a = ScheduleListView.SelectedItem;
            this.CurrentSchedule = (Schedule)e.AddedItems[0];
            this.DataContext = this.CurrentSchedule;

            CategoryEntriesCombo.SelectedIndex = -1;
            for (int i = 0; i < this.CategoryEntries.Count; i++)
            {
                if (this.CurrentSchedule.CategoryEntryId == this.CategoryEntries[i].Id) {
                    CategoryEntriesCombo.SelectedIndex = i;
                }
            }

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Schedule.Add(new data.Schedule {EmployeeId = this.Employee.Id, Day = this.nextDay(), CategoryEntryId=-1 , Id = -1});
            ScheduleListView.SelectedIndex = this.Schedule.Count - 1;
        }

        private DateTime nextDay() {
            DateTime day = DateTime.Now;
            foreach (Schedule schedule in this.Schedule)
            {
                if (schedule.Day.Date >= day.Date) {
                    day = schedule.Day.AddDays(1);
                }
            }

            return day;
        }

        private void CategoryEntriesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryEntriesCombo.SelectedItem != null) {
                this.CurrentSchedule.CategoryEntryId = ((TimeView.data.CategoryEntry)CategoryEntriesCombo.SelectedItem).Id;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate input
            bool valid = true;
            MessageTextBlock.Text = "";
            for (int i = 0; i < this.Schedule.Count; i++)
            {
                if (!Validate(this.Schedule[i])){
                    valid = false;
                    MessageTextBlock.Text = "Invalid data on line #" + (i + 1);
                    break;
                };
            }

            if (valid) {
                ScheduleGateway.addOrUpdate(this.Schedule.ToArray());
            }
        }

        private bool Validate(TimeView.data.Schedule entry) {
            if(entry.CategoryEntryId == -1){
                return false;
            }
            return true;
        }
    }
}
