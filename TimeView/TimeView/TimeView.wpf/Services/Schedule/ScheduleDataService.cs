using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.domain;

namespace TimeView.wpf.Services
{
    class ScheduleDataService : IScheduleDataService
    {
        IScheduleRepository repository;

        public ScheduleDataService(IScheduleRepository repository)
        {
            this.repository = repository;
        }

        public void CreateSchedule(Schedule schedule)
        {
            repository.CreateSchedule(schedule);
        }

        public async Task<Schedule[]> GetScheduleForEmployee(Employee employee)
        {
            return await repository.getScheduleForEmployee(employee.Id);
        }

        public void UpdateSchedule(Schedule schedule)
        {
            repository.UpdateSchedule(schedule);
        }
    }
}
