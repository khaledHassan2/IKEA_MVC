using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.Models.Departments
{
    public class DepartmentEditViewModel
    {
       // public int Id { get; set; }
        [Required(ErrorMessage ="Code Is Requirde ):")]
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
        [Display(Name ="Date Of Creation")]
        public DateOnly CreationDate { get; set; }
    }
}
