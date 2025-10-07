using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project.Models
{
    public class EmployeeModel
    {
        [Key]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "Employee Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string EmpName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string EmpMail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        [DataType(DataType.Password)]
        public string EmpPassword { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        public string Status { get; set; }

        [Display(Name = "Profile Picture")]
        public string? Profile { get; set; }
        public string Role { get; set; }
    }
}