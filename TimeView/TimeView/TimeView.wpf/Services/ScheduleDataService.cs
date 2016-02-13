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

        public List<Schedule> getScheduleForEmployee(int EmployeeId)
        {
        //    return repository.getScheduleForEmployee(EmployeeId);

            // TODO TODO TODO TODO TODO TODO TODO TODO 
            Schedule schedule1 = new Schedule
            {
                Id = 1,
                CategoryEntryId = 1,
                Day = DateTime.Now,
                EmployeeId = 1
            };

            Schedule schedule2 = new Schedule
            {
                Id = 1,
                CategoryEntryId = 1,
                Day = DateTime.Now.AddDays(1),
                EmployeeId = 1
            };

            List<Schedule> schedules = new List<Schedule>();
            schedules.Add(schedule1);
            schedules.Add(schedule2);

            return schedules;
        }

        public void UpdateSchedule(Schedule schedule)
        {
            repository.UpdateSchedule(schedule);
        }
    }
}
