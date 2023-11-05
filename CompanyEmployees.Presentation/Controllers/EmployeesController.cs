using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public EmployeesController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var employees = _service.EmployeeService.GetEmployees(companyId,trackChanges:false);
            return Ok(employees);
        }
        [HttpGet("{employeeId:guid}",Name = "GetEmployeeByIdForCompany")]
        public IActionResult GetEmployee(Guid companyId, Guid employeeId)
        {
            var employee = _service.EmployeeService.GetEmployee(companyId, employeeId,trackChanges:false);
            return Ok(employee);
        }
        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            if (employee is null)
            {
                return BadRequest("Employee is null");
            }
            var employeeEntity = _service.EmployeeService.CreateEmployeeForCompany(companyId,employee,trackChanges: false);
            return CreatedAtRoute("GetEmployeeByIdForCompany",new {companyId, employeeId  = employeeEntity.Id}, employeeEntity);
        }
        [HttpDelete("{employeeId:guid}")]
        public IActionResult DeleteEmployeeInCompany(Guid companyId, Guid employeeId)
        {
            _service.EmployeeService.DeleteEmployee(companyId,employeeId,trackChanges:false);
            return NoContent();
        }
    }
}
