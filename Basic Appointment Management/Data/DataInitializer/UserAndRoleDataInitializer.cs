using Basic_Appointment_Management.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Basic_Appointment_Management.Data.DataInitializer
{
    public class UserAndRoleDataInitializer
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByEmailAsync("admin1@localhost").Result == null)
            {
                User user = new User();
                user.UserName = "admin1@localhost";
                user.Email = "admin1@localhost";
                

                IdentityResult result = userManager.CreateAsync(user, "Abc/pq074]").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }


            
        }

       
    }
}
