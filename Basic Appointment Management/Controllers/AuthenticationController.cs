using Azure;
using Basic_Appointment_Management.Data.DTOs;
using Basic_Appointment_Management.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Basic_Appointment_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private ApiResponseDTO _response;

        public AuthenticationController(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _response = new ApiResponseDTO();
        }


        [HttpPost]
        [Route("/register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]



        public async Task<IActionResult> Register([FromBody] UserDTO model)
        {

            try 
            {


                if (!ModelState.IsValid)
                {

                    return UnprocessableEntity(ModelState);
                }

                var userExists = await _userManager.FindByNameAsync(model.UserName);
                if (userExists != null)
                {
                    _response.Status = HttpStatusCode.BadRequest;


                    _response.Message = "User already exists!";
                    return BadRequest(_response);
                }
                    

                User user = new User()
                {


                    UserName = model.UserName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    _response.Errors = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        _response.Errors.Add(error.Description);
                    }

                    _response.Status = HttpStatusCode.BadRequest;
                    

                    return StatusCode(StatusCodes.Status400BadRequest, _response);

                }

                _response.Status = HttpStatusCode.Created;
                _response.Message = "User created successfully!";

                return StatusCode(StatusCodes.Status201Created, _response);
            }

            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.Message = "An error occurred while processing your request.";

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }


        }



        [HttpPost]
        [Route("/login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]


        public async Task<IActionResult> Login([FromBody] UserDTO model)
        {
            try
            {

                if (!ModelState.IsValid)
                {

                    return UnprocessableEntity(ModelState);
                }

                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    _response.Status = HttpStatusCode.BadRequest;


                    _response.Message = "No user exists with the name";

                    return BadRequest(_response);

                   
                }

                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    _response.Status = HttpStatusCode.BadRequest;


                    _response.Message = "Wrong Password";


                    return BadRequest(_response);

                }


                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(15),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                _response.Status = HttpStatusCode.OK;
                _response.Data = new {


                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                };

                return Ok(_response);
            }

            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.Message = "An error occurred while processing your request.";

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }





        }
    }
}
