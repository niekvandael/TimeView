using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.domain;

namespace TimeView.wpf.Services
{
    internal class ScheduleDataService : IScheduleDataService
    {
        private readonly IScheduleRepository _repository;

        public ScheduleDataService(IScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Schedule[]> GetScheduleForEmployee(Employee employee)
        {
            return await _repository.GetScheduleForEmployee(employee.Id);
        }

        public void UpdateSchedule(Schedule schedule)
        {
            _repository.UpdateSchedule(schedule);
        }

        void IScheduleDataService.CreateSchedule(Schedule schedule)
        {
            throw new NotImplementedException();
        }

        void IScheduleDataService.SaveSchedules(List<Schedule> schedules, Func<bool, bool> callback)
        {
            _repository.SaveSchedules(schedules, callback);
        }

        public void CreateSchedule(Schedule schedule)
        {
            _repository.CreateSchedule(schedule);
        }
    }
}