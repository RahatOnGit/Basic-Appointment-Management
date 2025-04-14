using AutoMapper;
using Azure;
using Basic_Appointment_Management.Data;
using Basic_Appointment_Management.Data.DTOs;
using Basic_Appointment_Management.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Basic_Appointment_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentDbContext _dbContext;
        private readonly IMapper _mapper;
        private ApiResponseDTO _response;
        public AppointmentsController(AppointmentDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _response = new();
        }

        [HttpPost]
        [Route("/appointments")]
        
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]




        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentCreationDTO model)
        {
            try
            {
                

                if (!ModelState.IsValid)
                {
                   
                    return UnprocessableEntity(ModelState);
                }

            

                var hasDoctor = await _dbContext.Doctors.FindAsync(model.DoctorId);

                if (hasDoctor == null)
                {
                   
                    _response.Status = HttpStatusCode.BadRequest;
                   

                    _response.Message = $"The doctor Id={model.DoctorId} doesn't exist";

                    return BadRequest(_response);
                }

                var data = _mapper.Map<Appointment>(model);

                await _dbContext.Appointments.AddAsync(data);
                await _dbContext.SaveChangesAsync();

                var appointmentData = _mapper.Map<AppointmentShowingDTO>(data);

                appointmentData.DoctorName = data.TheDoctor.Name;

                 _response.Data = appointmentData;
                 _response.Status = HttpStatusCode.Created;
                

                

                return CreatedAtRoute("GetById", new { id = data.Id }, _response);
            }

            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.Message = "An error occurred while processing your request.";

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }




        }


        [HttpGet]
        [Route("/appointments")]
        
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAppointments()
        {
            try
            {
                if (!User.IsInRole("Administrator"))
                {
                    _response.Status = HttpStatusCode.Forbidden;
                    _response.Message = "You don't have permission to access it";

                    return StatusCode(StatusCodes.Status403Forbidden, _response);

                }


                var allAppointments = await _dbContext.Appointments.OrderBy(x => x.DateAndTime).Include(c => c.TheDoctor).ToListAsync();

                var allData = new List<AppointmentShowingDTO>();

                foreach (var item in allAppointments)
                {
                    var singleData = _mapper.Map<AppointmentShowingDTO>(item);

                    singleData.DoctorName = item.TheDoctor.Name;

                    allData.Add(singleData);
                }

                _response.Data = allData;
                _response.Status = HttpStatusCode.OK;
                 

                

                return Ok(_response);
            }

            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.Message = "An error occurred while processing your request.";

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }





        }


        [HttpGet]
        [Route("/appointments/{id:int}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetAppointmentById(int id)
        {

            try
            {
                

                if (id <= 0)
                {

                    _response.Status = HttpStatusCode.BadRequest;


                    _response.Message = "Id must be greater than zero.";

                    return BadRequest(_response);
                }

                var data = await _dbContext.Appointments.Include(c => c.TheDoctor).FirstOrDefaultAsync(c => c.Id == id);

                if (data == null)
                {
                    _response.Status = HttpStatusCode.NotFound;


                    _response.Message = $"No Appointment is found with Id = {id}";

                    return NotFound(_response);
                }

                var appointmentData = _mapper.Map<AppointmentShowingDTO>(data);

                appointmentData.DoctorName = data.TheDoctor.Name;

                _response.Data = appointmentData;
                _response.Status = HttpStatusCode.OK;
                

                return Ok(_response);
            }

            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.Message = "An error occurred while processing your request.";

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }





        }


        [HttpPut]
        [Route("/appointments/{id:int}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]


        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentCreationDTO model)
        {
            try
            {
                

                if (id <= 0)
                {
                    _response.Status = HttpStatusCode.BadRequest;


                    _response.Message = "Id must be greater than zero.";

                    return BadRequest(_response);
                }


                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(ModelState);
                }



                var hasDoctor = await _dbContext.Doctors.FindAsync(model.DoctorId);

                if (hasDoctor == null)
                {

                    _response.Status = HttpStatusCode.BadRequest;


                    _response.Message = "The doctor id doesn't exist";

                    return BadRequest(_response);
                    
                }

                var data = await _dbContext.Appointments.AsNoTracking().Where(c => c.Id == id).FirstOrDefaultAsync();

                if (data == null)
                {

                    _response.Status = HttpStatusCode.NotFound;


                    _response.Message = $"No Appointment is found with Id = {id}";

                    return NotFound(_response);

                   
                }

                data = _mapper.Map<Appointment>(model);

                data.Id = id;

                _dbContext.Appointments.Update(data);
                await _dbContext.SaveChangesAsync();

                var afterUpdate = _mapper.Map<AppointmentShowingDTO>(data);

                _response.Status = HttpStatusCode.OK;
                
                _response.Data = afterUpdate;

                return Ok(_response);
            }

            catch (Exception ex)
            {
                _response.Status = HttpStatusCode.InternalServerError;
                _response.Message = "An error occurred while processing your request.";

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }





        }

        [HttpDelete]
        [Route("/appointments/{id:int}")]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
              

                if (id <= 0)
                {
                    _response.Status = HttpStatusCode.BadRequest;


                    _response.Message = "Id must be greater than zero.";

                    return BadRequest(_response);
                }

                var data = await _dbContext.Appointments.FindAsync(id);

                if (data == null)
                {

                    _response.Status = HttpStatusCode.NotFound;


                    _response.Message = $"Appointment not found with Id = {id}";

                    return NotFound(_response);
                    
                }

                _dbContext.Appointments.Remove(data);
                await _dbContext.SaveChangesAsync();


                _response.Status = HttpStatusCode.OK;
                _response.Message = "Appointment deleted Succuessfully.";
                

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
