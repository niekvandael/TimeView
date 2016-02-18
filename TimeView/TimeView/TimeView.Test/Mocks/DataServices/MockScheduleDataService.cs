using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.Test.Mocks.Repositories;
using TimeView.wpf.Services;

namespace TimeView.Test.Mocks.DataServices
{
    class MockScheduleDataService : IScheduleDataService
    {
        private MockScheduleRepository repository = new MockScheduleRepository();

        public void CreateSchedule(Schedule schedule)
        {
            repository.CreateSchedule(schedule);
        }

        public Task<Schedule[]> GetScheduleForEmployee(Employee employee)
        {
            return repository.getScheduleForEmployee(employee.Id);
        }

        public void SaveSchedules(List<Schedule> schedules, Func<bool, bool> callback)
        {
            repository.SaveSchedules(schedules, callback);
        }

        public void UpdateSchedule(Schedule schedule)
        {
            repository.UpdateSchedule(schedule);
        }
    }
}
