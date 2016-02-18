using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using TimeView.context;
using TimeView.data;

namespace TimeView.api.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TimeViewContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TimeViewContext context)
        {
            //
            // Add Category
            //
            var category1 = new Category {Name = "Colors", CategorieEntries = new List<CategoryEntry>()};

            //
            // Add Category entries
            //
            var categoryEntry1 = new CategoryEntry
            {
                Category = category1,
                Name = "gray",
                Start = DateTime.Parse("09:00"),
                End = DateTime.Parse("17:00")
            };
            var categoryEntry2 = new CategoryEntry
            {
                Category = category1,
                Name = "green",
                Start = DateTime.Parse("17:00"),
                End = DateTime.Parse("22:00")
            };

            category1.CategorieEntries.Add(categoryEntry1);
            category1.CategorieEntries.Add(categoryEntry2);

            context.Category.AddOrUpdate(
                c => c.Name,
                category1
                );

            //
            // Add companies
            //

            var comp1 = new Company {Name = "UZ Antwerpen", Employees = new List<Employee>()};
            var comp2 = new Company {Name = "UZ Leuven", Employees = new List<Employee>()};
            context.Company.AddOrUpdate(
                c => c.Name,
                comp1,
                comp2
                );

            context.Company.AddOrUpdate(
                c => c.Name,
                comp1,
                comp2
                );

            //
            // Add Employees
            //

            var emp1 = new Employee
            {
                Username = "chrissy",
                Password = "chrissy",
                Name = "Chrissy Steegen",
                CompanyId = comp1.Id,
                Company = comp1,
                Following = new List<Employee>(),
                Follower = new List<Employee>()
            };
            var emp2 = new Employee
            {
                Username = "niek",
                Password = "niek",
                Name = "Niek Vandael",
                CompanyId = comp2.Id,
                Company = comp2,
                Following = new List<Employee>(),
                Follower = new List<Employee>()
            };

            emp2.Following.Add(emp1);
            emp1.Follower.Add(emp2);

            comp1.Employees.Add(emp1);
            comp2.Employees.Add(emp2);

            context.Employee.AddOrUpdate(
                e => e.Name,
                emp1,
                emp2
                );

            context.Employee.AddOrUpdate(
                e => e.Name,
                emp2,
                emp1
                );

            var schedule1 = new Schedule
            {
                Day = DateTime.Now,
                EmployeeId = 1,
                CategoryEntry = categoryEntry1,
                CategoryEntryId = categoryEntry1.Id
            };
            var schedule2 = new Schedule
            {
                Day = DateTime.Now.AddDays(1),
                EmployeeId = 1,
                CategoryEntry = categoryEntry2,
                CategoryEntryId = categoryEntry2.Id
            };

            context.Schedule.AddOrUpdate(
                s => s.Day,
                schedule1,
                schedule2
                );


            context.SaveChanges();
        }
    }
}