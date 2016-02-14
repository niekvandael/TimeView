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
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TimeView.context.TimeViewContext context)
        {
           
            
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

            Company comp1 = new Company { Name = "UZ Antwerpen"};
            Company comp2 = new Company { Name = "UZ Leuven"};
            context.Company.AddOrUpdate(
             c => c.Name,
                comp1,
                comp2
             );

            context.Company.AddOrUpdate(comp1);
            context.Company.AddOrUpdate(comp2);
            
            //
            // Add Employees
            //

            Employee emp1 = new Employee {Username = "chrissy", Password = "chrissy",Name = "Chrissy Steegen", CompanyId = comp1.Id, Company=comp1, Following = new List<Employee>() };
            Employee emp2 = new Employee { Username = "niek" , Password = "niek", Name = "Niek Vandael", CompanyId = comp2.Id, Company =comp2, Following = new List<Employee>() };

            emp2.Following.Add(emp1);

            context.Employee.AddOrUpdate(
             e => e.Name,
                emp1,
                emp2
             );

            Schedule schedule1 = new Schedule { Day = DateTime.Now, EmployeeId = 1, CategoryEntry = categoryEntry1, CategoryEntryId = categoryEntry1.Id };
            Schedule schedule2 = new Schedule { Day = DateTime.Now.AddDays(1), EmployeeId = 1, CategoryEntry = categoryEntry2, CategoryEntryId = categoryEntry2.Id };

            context.Schedule.AddOrUpdate(
                s => s.Day,
                schedule1,
                schedule2
            );


            context.SaveChanges();

        }
    }
}
