using Basic_Appointment_Management.Data.Config;
using Basic_Appointment_Management.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Basic_Appointment_Management.Data
{
    public class AppointmentDbContext : IdentityDbContext<User>
    {
        public AppointmentDbContext(DbContextOptions<AppointmentDbContext> options) : base(options)
        {
            
        }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DoctorConfig());

            modelBuilder.ApplyConfiguration(new RoleConfig());
        }
    }
}
