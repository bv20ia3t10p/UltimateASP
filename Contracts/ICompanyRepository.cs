using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Models;

namespace Contracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Company GetCompany(Guid id,bool trackChanges);
        void CreateCompany(Company company);
        IEnumerable<Company> GetCompaniesByIds(IEnumerable<Guid> ids,bool trackChanges);
        void DeleteCompany(Company company);
    }
}
