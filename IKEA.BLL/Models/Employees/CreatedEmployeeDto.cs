using IKEA.DAL.Common.Enum;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IKEA.BLL.Models.Employees
{
    public class CreatedEmployeeDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(20, ErrorMessage = "Max length of Name is 20 characters")]
        [MinLength(3, ErrorMessage = "Min length of Name is 3 characters")]
        public string Name { get; set; } = null!;

        [Range(22, 30, ErrorMessage = "Age must be between 22 and 30")]
        public int? Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; } = null!;

        
        [Range(1000, 100000, ErrorMessage = "Salary must be between 1,000 and 100,000")]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

       
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;

       
        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Hiring Date is required")]
        [Display(Name = "Hiring Date")]
        [DataType(DataType.Date)]
        public DateTime HiringDate { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Employee Type is required")]
        [Display(Name = "Employee Type")]
        public EmployeeType EmployeeType { get; set; }
        [Display (Name ="Department")]
        public int? DepartmentId { get; set; }
        public IFormFile? Image { get; set; }

    }
}
