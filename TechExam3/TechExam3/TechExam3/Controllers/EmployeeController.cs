using Microsoft.AspNetCore.Mvc;
using TechExam3.Interfaces;
using TechExam3.Model;
using TechExam3.Repository;
using TechExam3.Validator;

namespace TechExam3.Controllers
{
    [ApiController]
    [Route("api/")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeValidator _employeeValidator;
        public EmployeeController(IEmployeeRepository employeeRepository, IEmployeeValidator employeeValidator)
        {
            _employeeRepository = employeeRepository;
            _employeeValidator = employeeValidator;

        }
        // GET: EmployeeController/GetEmployee
        [HttpGet("GetEmployeeList")]
        public IActionResult GetEmployeeList(int id)
        {
            try
            {
                return Ok(_employeeRepository.GetEmployee(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        // POST: EmployeeController/CreateEmployee
        [HttpPost("CreateEmployee")]
        public IActionResult CreateEmployee(AddEmployeeRequest addEmployeeRequest)
        {
            try
            {
                var validation = _employeeValidator.ValidateRequest(addEmployeeRequest);
                if (validation.HasError) 
                {
                    return BadRequest(validation.ErrorMessage);
                }
                return Ok(_employeeRepository.AddEmployee(addEmployeeRequest));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        // POST: EmployeeController/EditEmployee
        [HttpPost("EditEmployee")]
        public async Task<IActionResult> EditEmployee(EditEmployeeRequest editEmployeeRequest)
        {
            var validation = _employeeValidator.ValidateRequest(editEmployeeRequest);
            if (validation.HasError)
            {
                return BadRequest(validation.ErrorMessage);
            }
            var item = _employeeRepository.GetEmployee(editEmployeeRequest.Id);
            if (item == null) return NotFound();
            var result = _employeeRepository.EditEmployee(editEmployeeRequest);
            return Ok(result);
        }
        // POST: EmployeeController/DeleteEmployee
        [HttpPost("DeleteEmployee")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                _employeeRepository.DeleteEmployee(id);
                return Ok("Employee Deleted!");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

    }
}
