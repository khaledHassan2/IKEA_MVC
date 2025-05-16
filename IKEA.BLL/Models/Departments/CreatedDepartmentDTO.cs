using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Models.Departments
{
    public class CreatedDepartmentDTO
    {
        [Required(ErrorMessage ="Code Is Required!")]
        public string Code { get; set; } = null!;
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(15, ErrorMessage = "Max length of Name is 15 characters")]
        [MinLength(2, ErrorMessage = "Min length of Name is 2 characters")]

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Display(Name ="Date Of Creation")]
        public DateOnly CreationDate { get; set; }
       
    }
}
