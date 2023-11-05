using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Models.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            var employeesFromDb = _repository.Employee.GetEmployees(companyId, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            return employeesDto;
        }
        public EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            var employee = _repository.Employee.GetEmployee(companyId, employeeId, trackChanges);
            if (employee == null)
            {
                throw new EmployeeNotFoundException(employeeId);
            }
            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }
        public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employee, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            var employeeEntity = _mapper.Map<Employee>(employee);
            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            _repository.Save();
            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employeeEntity);
            return employeeDto;
        }
        public void DeleteEmployee(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            var employee = _repository.Employee.GetEmployee(companyId, employeeId, trackChanges);
            if (employee is null)
            {
                throw new EmployeeNotFoundException(employeeId);
            }
            _repository.Employee.DeleteEmployee(employee);
            _repository.Save();
        }
        public void UpdateEmployee(Guid companyId, Guid employeeId, EmployeeForUpdateDto employee, bool compTrackChanges, bool empTrackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, compTrackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            var employeeInDb = _repository.Employee.GetEmployee(companyId, employeeId, empTrackChanges);
            if (employeeInDb is null)
            {
                throw new EmployeeNotFoundException(employeeId);
            }
            _mapper.Map(employee, employeeInDb);
            //employeeInDb.Position = employee.Position;
            //employeeInDb.Age = employee.Age;
            //employeeInDb.Name = employee.Name;
            _repository.Save();

        }
        public (EmployeeForUpdateDto employeeToPatch,Employee employeeEntity) GetEmployeeForPatch(Guid companyId,Guid id,
            bool compTrackChanges, bool empTrackChanges
            )
        {
            var company = _repository.Company.GetCompany(companyId, compTrackChanges);
            if (company == null) throw new CompanyNotFoundException(companyId);
            var employeeEntity = _repository.Employee.GetEmployee(companyId,id, empTrackChanges);
            if (employeeEntity == null) throw new EmployeeNotFoundException(id);
            var empToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);
            return (empToPatch, employeeEntity);
        }
        public void saveChangesForPatch(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
        {
            _mapper.Map(employeeToPatch, employeeEntity);
            _repository.Save();
        }
    }
}
