﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Basic_Appointment_Management.Data.Config
{
    public class RoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
           
             builder.HasData(
             new IdentityRole
             {
                  Name = "General_User",
                  NormalizedName = "GENERAL_USER"
             },
             new IdentityRole
             {
                 Name = "Manager",
                 NormalizedName = "MANAGER"
             },
             new IdentityRole
             {
                 Name = "Administrator",
                 NormalizedName = "ADMINISTRATOR"
             }
             );

        }
    }
}
