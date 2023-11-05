using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);
        EmployeeDto GetEmployee(Guid companyId,Guid employeeId, bool trackChanges);
        EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employee, bool trackChanges);
        void DeleteEmployee(Guid companyId, Guid employeeId, bool trackChanges);
        void UpdateEmployee(Guid companyId, Guid employeeId, EmployeeForUpdateDto employee,bool compTrackChanges, bool empTrackChanges);
        void saveChangesForPatch(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);
        (EmployeeForUpdateDto employeeToPatch, Employee employeeEntity) GetEmployeeForPatch(Guid companyId, Guid id,
           bool compTrackChanges, bool empTrackChanges
           );
    }
}
