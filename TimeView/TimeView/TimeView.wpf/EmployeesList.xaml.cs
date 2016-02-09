using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeView.data;
using TimeView.data.Services;

namespace TimeView.wpf
{

    public partial class EmployeesList : Page
    {

        public EmployeesList()
        {
            InitializeComponent();
            this.getData();
        }

        public async Task getData(){
            Employee[] employees = await WebAPIGateway.getEmployees();
            int a = 5;
        }

    }
}
