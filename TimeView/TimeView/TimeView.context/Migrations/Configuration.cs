namespace TimeView.context.Migrations
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
            // Add companies
            //

            Company comp1 = new Company { Name = "UZ Antwerpen", Id = 0  };
            Company comp2 = new Company { Name = "UZ Leuven", Id = 1 };
            context.Company.AddOrUpdate(
             c => c.Name,
                comp1,
                comp2
             );

            //
            // Add Employees
            //

            Employee emp1 = new Employee { Name = "Chrissy Steegen", CompanyId = 0, Schedules = new List<Schedule>() };
            emp1.Schedules.Add(schedule1);

            Employee emp2 = new Employee { Name = "Liesbeth Ramaekers", CompanyId = 1, Schedules = new List<Schedule>() };
            emp2.Schedules.Add(schedule2);

            context.Employee.AddOrUpdate(
             e => e.Name,
                emp1,
                emp2
             );


            //
            // Add Followers
            //

            Follower follower1 = new Follower { Name = "Niek Vandael", Following = new List<Employee>()  };
            follower1.Following.Add(emp1);

            Follower follower2 = new Follower { Name = "Kris Hermans", Following = new List<Employee>()  };
            follower2.Following.Add(emp2);

            context.Follower.AddOrUpdate(
                f => f.Name,
                follower1,
                follower2
             );
            
            context.SaveChanges();

        }
    }
}
