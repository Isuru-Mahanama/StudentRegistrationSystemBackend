namespace StudentRegistrationSystem.Models.DTO
{
    public class ScheduleDTO
    {
        public int scheduleID { get; set; }
        public string courseCode { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string day { get; set; }
        public bool scheduleStatus { get; set; }
    }
}
