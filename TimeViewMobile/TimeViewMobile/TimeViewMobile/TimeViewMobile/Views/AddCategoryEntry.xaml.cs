﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.DAL.Repositories.Category;
using TimeView.DAL.Repositories.Employee;
using TimeView.DAL.Repositories.Schedule;
using TimeView.wpf.Services;
using TimeViewMobile.ViewModels;
using Xamarin.Forms;

namespace TimeViewMobile.Views
{
    public partial class AddCategoryEntry : ContentPage
    {
        public AddCategoryEntry()
        {
            InitializeComponent();


            //
            // Repositories
            //
            IEmployeeDataService EmployeeDataService = new EmployeeDataService(new EmployeeRepository());
            ICategoryEntryDataService CategoryEntryDataService = new CategoryEntryDataService(new CategoryEntryRepository());


            //
            // ListViews
            //
            AddCategoryEntryViewModel AddCategoryEntryViewModel = new AddCategoryEntryViewModel(EmployeeDataService, CategoryEntryDataService, CategoryEntryPicker);
            this.BindingContext = AddCategoryEntryViewModel;
        }
    }
}
