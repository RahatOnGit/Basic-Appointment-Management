using Basic_Appointment_Management.Data.DTOs;
using Basic_Appointment_Management.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Basic_Appointment_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class UserManageController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        public UserManageController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("/users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var AdminName = HttpContext.User.Identity.Name;
            var all_users = await _userManager.Users.ToListAsync();

            List<UsersDTO> the_data = new List<UsersDTO>();

            foreach (var user in all_users)
            {
                if (user.UserName!=AdminName)
                {
                    UsersDTO usersDTO = new UsersDTO();
                    usersDTO.UserName = user.UserName;

                    usersDTO.IsManager = await _userManager.IsInRoleAsync(user, "Manager");

                    the_data.Add(usersDTO);
                }

               

            } 

            return Ok(the_data);
        }

        [HttpPut]
        [Route("/users")]

        public async Task<IActionResult> UpdateUsers([FromBody] List<UsersDTO> all_users)
        {
            foreach(var update_user in all_users)
            {
                User theUser = await _userManager.FindByNameAsync(update_user.UserName);
                bool isCurrentlyManager = await _userManager.IsInRoleAsync(theUser, "Manager");

                if(isCurrentlyManager && !update_user.IsManager)
                {
                    await _userManager.RemoveFromRoleAsync(theUser, "Manager");
                }

                else if (!isCurrentlyManager && update_user.IsManager)
                {
                    await _userManager.AddToRoleAsync(theUser, "Manager");
                }
            }

            return Ok("ALHAMDULILLAH, users role updated.");

        }

    }
}
