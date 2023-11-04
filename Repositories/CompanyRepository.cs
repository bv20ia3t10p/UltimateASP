using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CompanyRepository: RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext) {
            
        }
        public IEnumerable<Company> GetAllCompanies(bool trackChanges) => GetAll(trackChanges).OrderBy(c => c.Name).ToList();
        public Company GetCompany(Guid id, bool trackChanges) => Get(c=>c.Id.Equals(id),trackChanges).SingleOrDefault();
    }
}
