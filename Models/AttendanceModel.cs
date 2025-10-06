using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class AttendanceModel
    {
        [Key]
        public int AttendId { get; set; }

        [Required(ErrorMessage = "Employee ID is required")]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "Attendance date is required")]
        [DataType(DataType.Date)]
        public DateTime AttendDate { get; set; }

        [Required(ErrorMessage = "Clock-in hour is required")]
        [Range(0, 23, ErrorMessage = "Clock-in hour must be between 0 and 23")]
        public int ClockInHour { get; set; }

        [Required(ErrorMessage = "Clock-in minute is required")]
        [Range(0, 59, ErrorMessage = "Clock-in minute must be between 0 and 59")]
        public int ClockInMin { get; set; }

        [Required(ErrorMessage = "Clock-out hour is required")]
        [Range(0, 23, ErrorMessage = "Clock-out hour must be between 0 and 23")]
        public int ClockOutHour { get; set; }

        [Required(ErrorMessage = "Clock-out minute is required")]
        [Range(0, 59, ErrorMessage = "Clock-out minute must be between 0 and 59")]
        public int ClockOutMin { get; set; }

        [Required(ErrorMessage = "Work type is required")]
        [StringLength(10, ErrorMessage = "Work type can only be Remote, Office, or Field")]
        public string WorkType { get; set; }

        [StringLength(100, ErrorMessage = "Task type cannot exceed 100 characters")]
        public string? TaskType { get; set; }

        [Required(ErrorMessage = "Working hours are required")]
        [Range(1, int.MaxValue, ErrorMessage = "Working hours must be greater than 0")]
        public int WorkingHour { get; set; }

        [StringLength(10)]
        public string AttendStatus { get; set; }
    }
}