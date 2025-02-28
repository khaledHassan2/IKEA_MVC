using IKEA.BLL.Models.Departments;
using IKEA.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<CreatedDepartmentDTO> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService,ILogger<CreatedDepartmentDTO> logger,IWebHostEnvironment environment
            )
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }
        //BaseUrl/Department/Index
        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }
        #endregion
        #region Create
        #region Get
        [HttpGet]
        //BaseUrl/Department/Create
        public IActionResult Create()
        {
            return View();
        }
        #endregion
        #region Post
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDTO departmentDTO)
        {
            string message = string.Empty;
            try
            {
                if (!ModelState.IsValid)// Server Side Validation
                {
                    return View(departmentDTO);
                }
                var Result = _departmentService.CreatDepartment(departmentDTO);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Department Is Not Created";
                    ModelState.AddModelError(string.Empty,message );
                    return View(departmentDTO);
                }
            }
            catch(Exception ex)
            {
                // 1-log Exception
                _logger.LogError(ex, ex.Message);
                // 2-set Frindly Message
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(departmentDTO);
                }
                else
                {
                    message = "Department Is Not Created";
                    return View("Error", message);
                }
            }



        }
        #endregion
        #endregion
    }
}
