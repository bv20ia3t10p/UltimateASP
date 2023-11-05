using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Contracts;
using Shared.DataTransferObjects;
using Microsoft.AspNetCore.JsonPatch;

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
        [HttpPut("{employeeId:guid}")]
        public IActionResult UpdateEmployeeInCompany(Guid companyId, Guid employeeId, [FromBody] EmployeeForUpdateDto employee)
        {
            _service.EmployeeService.UpdateEmployee(companyId, employeeId, employee, compTrackChanges: false, empTrackChanges:true);
            return NoContent();
        }
        [HttpPatch("{employeeId:guid}")]
        public IActionResult PartiallyUpdateEmployeeForCompany(Guid companyId,Guid employeeId, [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if (patchDoc is null) return BadRequest("patchDoc object sent is null");
            var result = _service.EmployeeService.GetEmployeeForPatch(companyId, employeeId, compTrackChanges: false, empTrackChanges: true);
            patchDoc.ApplyTo(result.employeeToPatch);
            _service.EmployeeService.saveChangesForPatch(result.employeeToPatch, result.employeeEntity);
            return NoContent();
        }
    }
}
