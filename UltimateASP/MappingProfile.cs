using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace UltimateASP
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Company, CompanyDto>()
                .ForCtorParam("FullAdress",
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));
        }
    }
}
