namespace TimeView.api.Migrations
{
    using data;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TimeView.context.TimeViewContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TimeView.context.TimeViewContext context)
        {
            //
            // Add Schedules
            //
            Schedule schedule1 = new Schedule { Start = DateTime.Parse("2016-01-01 07:00"), End = DateTime.Parse("2016-01-01 09:00") };
            Schedule schedule2 = new Schedule { Start = DateTime.Parse("2016-01-02 14:00"), End = DateTime.Parse("2016-01-01 22:00") };
            context.Schedule.AddOrUpdate(
             s => s.Start,
                schedule1,
                schedule2
             );

            //
            // Add Category
            //
            Category category1 = new Category { Name = "Colors", CategorieEntries = new List<CategoryEntry>() };

            //
            // Add Category entries
            //
            CategoryEntry categoryEntry1 = new CategoryEntry { Category = category1, Name = "gray", Start = TimeSpan.Parse("09:00"), End = TimeSpan.Parse("17:00") };
            CategoryEntry categoryEntry2 = new CategoryEntry { Category = category1, Name = "green", Start = TimeSpan.Parse("17:00"), End = TimeSpan.Parse("22:00") };

            category1.CategorieEntries.Add(categoryEntry1);
            category1.CategorieEntries.Add(categoryEntry2);

            context.Category.AddOrUpdate(category1);

            //
            // Add companies
            //

            Company comp1 = new Company { Name = "UZ Antwerpen", Id = 0 };
            Company comp2 = new Company { Name = "UZ Leuven", Id = 1 };
            context.Company.AddOrUpdate(
             c => c.Name,
                comp1,
                comp2
             );

            //
            // Add Employees
            //

            Employee emp1 = new Employee { Name = "Chrissy Steegen", CompanyId = 0, Schedules = new List<Schedule>(), Followers = new List<Employee>() };
            emp1.Schedules.Add(schedule1);

            Employee emp2 = new Employee { Name = "Niek Vandael", CompanyId = 1, Schedules = new List<Schedule>(), Followers = new List<Employee>() };
            emp2.Schedules.Add(schedule2);

            emp1.Followers.Add(emp2);

            context.Employee.AddOrUpdate(
             e => e.Name,
                emp1,
                emp2
             );

            context.SaveChanges();

        }
    }
}
