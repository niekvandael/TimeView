using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.wpf.Services
{
    public interface IScheduleDataService
    {
        List<Schedule> getScheduleForEmployee(int EmployeeId);
        void CreateSchedule(Schedule schedule);
        void UpdateSchedule(Schedule schedule);
    }
}
