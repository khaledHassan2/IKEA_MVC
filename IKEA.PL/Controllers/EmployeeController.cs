using IKEA.BLL.Models.Departments;
using IKEA.BLL.Models.Employees;
using IKEA.BLL.Services.Departments;
using IKEA.BLL.Services.Employees;
using IKEA.PL.Models.Departments;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<EmployeeController> _logger;
       

        public EmployeeController(
           IEmployeeService employeeService,
           IWebHostEnvironment webHostEnvironment,
           ILogger<EmployeeController> logger,IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
           
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
        }
        #endregion

        #region Create
        #region GET
        [HttpGet]
        public IActionResult Create()
        {
          
            return View();
        }
        #endregion

        #region POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            string message = string.Empty;

            try
            {
                if (!ModelState.IsValid)
                {
                    // Log model validation errors
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            _logger.LogWarning($"Validation error in '{state.Key}': {error.ErrorMessage}");
                        }
                    }

                    return View(employeeDto);
                }

                var result = _employeeService.CreateEmployee(employeeDto);

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Employee was not created.";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employeeDto);
                }
            }
            catch (Exception ex)
            {
                // Log exception
                _logger.LogError(ex, "Exception occurred while creating an employee. Message: {Message}, Inner: {Inner}", ex.Message, ex.InnerException?.Message);

                if (_webHostEnvironment.IsDevelopment())
                {
                    message = ex.Message + " - " + ex.InnerException?.Message;
                    ModelState.AddModelError(string.Empty, message);
                    return View(employeeDto);
                }

                // Return user-friendly error
                if (_webHostEnvironment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeDto);
                }
                else
                {
                    message = "An unexpected error occurred. Employee was not created.";
                    return View("Error", message);
                }
            }
        }
        #endregion
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest();

            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null) return NotFound();//404

            return View(employee);
        }
        #endregion
        #region Edit
        #region Get
        [HttpGet]
        public IActionResult Edit(int? id, [FromServices]IDepartmentService departmentService)
        {
            if (id is null) return BadRequest();

            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null) return NotFound();//404
            ViewData["Departments"] = departmentService.GetAllDepartments();

            return View(new UpdatedEmployeeDto()
            {
               Name=employee.Name,
               Address=employee.Address,
               Email=employee.Email,
               Age=employee.Age,
               Salary=employee.Salary,
               PhoneNumber=employee.PhoneNumber,
               IsActive=employee.IsActive,
               EmployeeType=employee.EmployeeType,
               Gender=employee.Gender,
               HiringDate=employee.HiringDate
            });

        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, UpdatedEmployeeDto updatedEmployeeDto)
        {
            if (!ModelState.IsValid) return View(updatedEmployeeDto);
            var message = string.Empty;
            try
            {
               
                var updeted = _employeeService.UpdateEmployee(updatedEmployeeDto) > 0;
                if (updeted)
                {
                    return RedirectToAction(nameof(Index));
                }
                message = "Sorry,An Error ";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Sorry,An Error ";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(updatedEmployeeDto);

        }
        #endregion
        #endregion
        #region Delete
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            var delete = _employeeService.DeleteEmployee(id);
            try
            {
                if (delete)
                {
                    return RedirectToAction(nameof(Index));
                }
                message = "Sorry,An Error ";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Sorry,An Error ";
            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));

        }
        #endregion
        #endregion
    }
}
