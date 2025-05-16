using IKEA.BLL.Models.Departments;
using IKEA.BLL.Services.Departments;
using IKEA.DAL.Models.Departments;
using IKEA.PL.Models.Departments;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<CreatedDepartmentDTO> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService, ILogger<CreatedDepartmentDTO> logger, IWebHostEnvironment environment
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
            //1: ViewData = is dictionary type property it helps us transfer the data from conttroler [action] to view
            ViewData["Message"] = "Hello View Data";
            //2: ViewBag=> Dynamic type prop it helps us transfer the data from conttroler [action] to view
            ViewBag.Message = "Hello View Bag";
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
        [ValidateAntiForgeryToken]
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
                // 3: TempData is prop of type Dictionary obj used for transefring the data between 2 requests
                if (Result > 0)
                {
                    TempData["Message"] = "Department is Created";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Department Is Not Created";
                    message = "Department Is Not Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentDTO);
                }
            }
            catch (Exception ex)
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
        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest();

            var department = _departmentService.GetDepartmentByID(id.Value);

            if (department is null) return NotFound();//404

            return View(department);
        }
        #endregion
        #region Edit
        #region Get
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest();

            var department = _departmentService.GetDepartmentByID(id.Value);

            if (department is null) return NotFound();//404

            return View(new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate
            });

        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, DepartmentEditViewModel departmentVM)
        {
            var message = string.Empty;
            if (!ModelState.IsValid) return View(departmentVM);
            try
            {
                var upDepartment = new UpdatedDepartmentDTO()
                {
                    Id=id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate
                };
                var updeted = _departmentService.UpdateDepartment(upDepartment) > 0;
                if (updeted) 
                {
                    return RedirectToAction(nameof(Index));
                }
                message = "Sorry,An Error ";
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment()? ex.Message : "Sorry,An Error ";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);

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
            var delete = _departmentService.DeleteDepartment(id);
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
                message = _environment.IsDevelopment() ? ex.Message : "Sorry,An Error ";
            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));

        }
        #endregion
        #endregion
    }
}