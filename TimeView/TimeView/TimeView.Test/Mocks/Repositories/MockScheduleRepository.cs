using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.domain;

namespace TimeView.Test.Mocks.Repositories
{
    internal class MockScheduleRepository : IScheduleRepository
    {
        public void CreateSchedule(Schedule schedule)
        {
            throw new NotImplementedException();
        }

        public Task<Schedule[]> GetScheduleForEmployee(int employeeId)
        {
            var schedule = new List<Schedule>();

            var schedule1 = new Schedule
            {
                Id = 1,
                CategoryEntryId = 1,
                CategoryEntry = new CategoryEntry
                {
                    Id = 1,
                    CategoryId = 1,
                    Start = DateTime.Parse("09:00"),
                    End = DateTime.Parse("17:00"),
                    Name = "gray"
                },
                Day = DateTime.Now,
                EmployeeId = employeeId
            };
            var schedule2 = new Schedule
            {
                Id = 2,
                CategoryEntryId = 2,
                CategoryEntry = new CategoryEntry
                {
                    Id = 2,
                    CategoryId = 1,
                    Start = DateTime.Parse("14:00").Date,
                    End = DateTime.Parse("22:00").Date,
                    Name = "gray"
                },
                Day = DateTime.Now.AddDays(1),
                EmployeeId = employeeId
            };

            schedule.Add(schedule1);
            schedule.Add(schedule2);

            return Task.FromResult(schedule.ToArray());
        }

        public void SaveSchedules(List<Schedule> schedules, Func<bool, bool> callback)
        {
            throw new NotImplementedException();
        }

        public void UpdateSchedule(Schedule schedule)
        {
            throw new NotImplementedException();
        }
    }
}