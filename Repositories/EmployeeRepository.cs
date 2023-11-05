using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) => Get(e => e.CompanyId.Equals(companyId), trackChanges).OrderBy(e => e.Name).ToList();
        public Employee GetEmployee(Guid companyId, Guid employeeId, bool trackChanges) =>
            Get(e => e.Id.Equals(employeeId) && e.CompanyId.Equals(companyId), trackChanges).SingleOrDefault();
        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }
        public void DeleteEmployee(Employee employee) => Delete(employee);
    }
}
