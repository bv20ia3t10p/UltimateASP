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
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {
            try
            {
                var companies = _repository.Company.GetAllCompanies(trackChanges);
                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
                return companiesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllCompanies)} service method {ex}");
                throw;
            }
        }
        public CompanyDto GetCompany(Guid id, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(id, trackChanges);
            if (company == null)
            {
                throw new CompanyNotFoundException(id);
            }
            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }
        public CompanyDto CreateCompany(CompanyForCreationDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);
            _repository.Company.CreateCompany(companyEntity);
            _repository.Save();
            var returnCompany = _mapper.Map<CompanyDto>(companyEntity);
            return returnCompany;
        }
        public IEnumerable<CompanyDto> GetCompaniesByIds(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids == null)
            {
                throw new IdParametersBadRequestException();
            }
            var companyEntities = _repository.Company.GetCompaniesByIds(ids,trackChanges);
            if (ids.Count() != companyEntities.Count())
            {
                throw new CollectionByIdsBadREquestException();
            }
            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            return companiesToReturn;
        }
        public (IEnumerable<CompanyDto> companies, string ids) CreateCompaniesCollection (IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if (companyCollection == null) {
                throw new CompanyCollectionBadRequest();
            }
            var companiesEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach ( var company in companiesEntities)
            {
                _repository.Company.CreateCompany(company);
            }
            _repository.Save();
            var companyCollectionToRerturn = _mapper.Map<IEnumerable<CompanyDto>>(companiesEntities);
            var ids = string.Join(",",companyCollectionToRerturn.Select(company => company.Id));
            return (companyCollectionToRerturn,ids);
        }
        public void DeleteCompany(Guid companyId, bool trackChanges)
        {
            var companyInDb = _repository.Company.GetCompany(companyId, trackChanges);
            if (companyInDb == null)
            {
                throw new CompanyNotFoundException(companyId);
            }
            _repository.Company.DeleteCompany(companyInDb);
            _repository.Save();
        }
    }
}
