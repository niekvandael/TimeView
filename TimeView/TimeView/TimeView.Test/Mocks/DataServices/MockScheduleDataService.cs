using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.Test.Mocks.Repositories;
using TimeView.wpf.Services;

namespace TimeView.Test.Mocks.DataServices
{
    internal class MockScheduleDataService : IScheduleDataService
    {
        private readonly MockScheduleRepository _repository = new MockScheduleRepository();

        public void CreateSchedule(Schedule schedule)
        {
            _repository.CreateSchedule(schedule);
        }

        public Task<Schedule[]> GetScheduleForEmployee(Employee employee)
        {
            return _repository.GetScheduleForEmployee(employee.Id);
        }

        public void SaveSchedules(List<Schedule> schedules, Func<bool, bool> callback)
        {
            _repository.SaveSchedules(schedules, callback);
        }

        public void UpdateSchedule(Schedule schedule)
        {
            _repository.UpdateSchedule(schedule);
        }
    }
}