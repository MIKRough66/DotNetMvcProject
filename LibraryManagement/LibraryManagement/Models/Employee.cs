using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;             // for [Column]

namespace LibraryManagement.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        [Required]
        [Display(Name="Employee Name")]
        [Column("Name")]
        [StringLength(50, MinimumLength =5, ErrorMessage ="Name must be between 5 to 50 characters")]
        public string EmployeeName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Cell Phone")]
        [DataType(DataType.PhoneNumber)]
        public string CellPhone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Joining Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime JoiningDate { get; set; }
    }
}