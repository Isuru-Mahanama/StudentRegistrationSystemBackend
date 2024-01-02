using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentRegistrationSystem.Models.Domain;
using StudentRegistrationSystem.Models.DTO;
using StudentRegistrationSystem.Repository.Implementation;
using StudentRegistrationSystem.Repository.Interface;

namespace StudentRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository scheduleRepository;

        public ScheduleController(IScheduleRepository scheduleRepository)
        {
            this.scheduleRepository = scheduleRepository;
        }

        [HttpPost("admin/schedule")]
        public async Task<ActionResult<Schedulecs>> AddingSchedule([FromBody] ScheduleDTO schedulecs)
        { 
           

            var course = await scheduleRepository.saveSchedules(schedulecs);
            return Ok(course);
        }


        [HttpGet]
        [Route("GetAllSchedules")]
        public List<Schedulecs> GetAllCourses()
        {

            List<Schedulecs> schedulecs = scheduleRepository.GetSchedulecs();
            return schedulecs;

        }

        [HttpGet]
        [Route("admin/getScheduleByID")]  // Corrected the spelling in the Route attribute
        public async Task<Schedulecs> getScheduleByID(int scheduleID)
        {

            Schedulecs schedulecs = await scheduleRepository.getScheduleByID(scheduleID);  // Corrected the variable name
            return schedulecs;  // Return the updated course, not the input parameter
        }
        [HttpDelete]
        [Route("admin/getScheduleDelete")]  // Corrected the spelling in the Route attribute
        public async Task<Schedulecs> deleteSchedule(int scheduleID)
        {

            Schedulecs schedulecs = await scheduleRepository.deleteSchedule(scheduleID);  // Corrected the variable name
            return schedulecs;  // Return the updated course, not the input parameter
        }

        [HttpPut]
        [Route("admin/updateSceduleDetails")]  // Corrected the spelling in the Route attribute
        public async Task<Schedulecs> UpdatingSchedules([FromBody] ScheduleDTO schedulecs)
        {
            Schedulecs updatedSchedules = await scheduleRepository.updateSchedules(schedulecs);  // Corrected the variable name
            return updatedSchedules;  // Return the updated course, not the input parameter
        }
    }
}
