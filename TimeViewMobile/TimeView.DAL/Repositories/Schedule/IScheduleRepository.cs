using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeView.DAL.Repositories.Schedule
{
    public interface IScheduleRepository
    {
        Task<TimeView.data.Schedule[]> GetScheduleForEmployee(int employeeId);
        void CreateSchedule(TimeView.data.Schedule schedule);
        void UpdateSchedule(TimeView.data.Schedule schedule);
        void SaveSchedules(List<TimeView.data.Schedule> schedules, Func<bool, bool> callback);
    }
}