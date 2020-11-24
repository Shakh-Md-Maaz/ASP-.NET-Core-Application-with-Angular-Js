using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreOrderManagementProject.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>().HasKey(s => s.PatientId);
            modelBuilder.Entity<Doctor>().HasKey(s => s.DoctorId);

            modelBuilder.Entity<Patient>().HasOne(s => s.Doctor).WithMany(s => s.Patients)
                .HasForeignKey(s => s.DoctorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
