using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using TimeView.data;

namespace TimeView.context
{
    public class TimeViewContext : DbContext
    {
        public TimeViewContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Company> Company { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryEntry> CategoryEntry { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Set PK's
            modelBuilder.Entity<Company>().HasKey(e => e.Id);
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Schedule>().HasKey(e => e.Id);
            modelBuilder.Entity<Category>().HasKey(e => e.Id);
            modelBuilder.Entity<CategoryEntry>().HasKey(e => e.Id);

            // Many to Many
            modelBuilder.Entity<Employee>().HasMany(m => m.Follower)
                .WithMany(m => m.Following)
                .Map(x => x.MapLeftKey("EmployeeId")
                    .MapRightKey("FollowerId")
                    .ToTable("UserFollowers"));

            // One to Many
            modelBuilder.Entity<Employee>()
                .HasRequired(e => e.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.CompanyId);

            // Set column name of Day to date
            modelBuilder
                .Entity<Schedule>()
                .Property(f => f.Day)
                .HasColumnType("date");

            // Unique Key constaint
            modelBuilder
                .Entity<Schedule>()
                .Property(s => s.Day)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IndexEmployeeIdDay", 1) {IsUnique = true, Order = 1}));

            modelBuilder
                .Entity<Schedule>()
                .Property(s => s.EmployeeId)
                .IsRequired()
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IndexEmployeeIdDay", 2) {IsUnique = true, Order = 2}));
        }
    }
}