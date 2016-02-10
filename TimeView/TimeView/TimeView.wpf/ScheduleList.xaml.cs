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

        public ScheduleList(Employee employee)
        {
            InitializeComponent();
            this.Employee = employee;
            this.getData();
        }

        public async Task getData()
        {
            this.Schedule = new ObservableCollection<Schedule>(await ScheduleGateway.getScheduleForEmployee(this.Employee.Id));
            ScheduleListView.ItemsSource = this.Schedule;
        }
    }
}
