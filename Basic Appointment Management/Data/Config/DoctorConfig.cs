using Basic_Appointment_Management.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basic_Appointment_Management.Data.Config
{
    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.HasKey(x => x.Id);


            builder.Property(n => n.Name).HasMaxLength(250);

            builder.HasData(new List<Doctor>()
            {
               new Doctor() {
                   Id = 1,
                   Name = "Mohammad Ali"
                   },

               new Doctor() {
                   Id = 2,
                   Name = "Mohammad Ibrahim Hossain"
                   }



            });


        }
    }

}

