using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.wpf.Services
{
    public interface IScheduleDataService
    {
        Task<Schedule[]> GetScheduleForEmployee(Employee employee);
        void UpdateSchedule(Schedule schedule);
        void CreateSchedule(Schedule schedule);
        void SaveSchedules(List<Schedule> schedules, Func<bool, bool> callback);
    }
}