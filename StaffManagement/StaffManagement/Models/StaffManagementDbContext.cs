using Microsoft.EntityFrameworkCore;

namespace StaffManagement.Models
{
    public class StaffManagementDbContext : DbContext
    {
        public StaffManagementDbContext(DbContextOptions<StaffManagementDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer()

            // optionsBuilder.UseSqlServer(@"Server=.\SQLExpress;Database=test;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager)
                .WithMany()
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Department>().HasOne(e => e.Parent)
              .WithMany(d => d.ChildDepartments)
              .HasForeignKey(e => e.ParentId)
              .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        //entities
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }


    }
}
