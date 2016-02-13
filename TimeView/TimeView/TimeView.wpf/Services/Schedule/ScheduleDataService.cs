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

        public async Task<Schedule[]> GetScheduleForEmployee(int EmployeeId)
        {
            return await repository.getScheduleForEmployee(EmployeeId);
        }

        public void UpdateSchedule(Schedule schedule)
        {
            repository.UpdateSchedule(schedule);
        }
    }
}
