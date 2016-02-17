using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.domain
{
    public interface IScheduleRepository
    {
        System.Threading.Tasks.Task<Schedule[]> getScheduleForEmployee(int EmployeeId);
        void CreateSchedule(Schedule schedule);
        void UpdateSchedule(Schedule schedule);
        void SaveSchedules(List<Schedule> schedules);
    }
}
