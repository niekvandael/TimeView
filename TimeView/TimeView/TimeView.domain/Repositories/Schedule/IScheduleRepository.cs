using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.domain
{
    public interface IScheduleRepository
    {
        Task<Schedule[]> GetScheduleForEmployee(int employeeId);
        void CreateSchedule(Schedule schedule);
        void UpdateSchedule(Schedule schedule);
        void SaveSchedules(List<Schedule> schedules, Func<bool, bool> callback);
    }
}