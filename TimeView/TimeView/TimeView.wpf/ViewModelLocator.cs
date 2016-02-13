﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data.Services;
using TimeView.wpf.Services;
using TimeView.wpf.ViewModel;

namespace TimeView.wpf
{
    public class ViewModelLocator
    {
        private static IScheduleDataService scheduleDataService = new ScheduleDataService(new ScheduleRepository());
        private static ICategoryEntryDataService categoryEntryDataService = new CategoryEntryDataService(new CategoryEntryRepository());

        private static ScheduleListViewModel scheduleListViewModel = new ScheduleListViewModel(scheduleDataService, categoryEntryDataService);
        public static ScheduleListViewModel ScheduleListViewModel
        {
            get
            {
                return scheduleListViewModel;
            }
        }
    }
}
