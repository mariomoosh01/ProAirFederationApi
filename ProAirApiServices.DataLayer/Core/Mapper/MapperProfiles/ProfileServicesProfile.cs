using AutoMapper;
using ProAirApiServices.DataLayer.DataAccess.Entities;
using ProAirApiServices.DataLayer.Models.Dto.Login;
using ProAirApiServices.DataLayer.Models.Dto.Members;
using ProAirApiServices.DataLayer.Models.Dto.Profile;

namespace ProAirApiServices.DataLayer.Core.Mapper.MapperProfiles
{
    internal class ProfileServicesProfile: Profile
    {
        public ProfileServicesProfile()
        {
            CreateMappers();
        }

        private void CreateMappers()
        {
            CreateMap<States, StatesDto>();
            CreateMap<Members, MemberDto>();
            CreateMap<MemberDto, Members>();
            CreateMap<MembersCreditCards, MembersCreditCardsDto>();               
            CreateMap<MembersCreditCardsDto, MembersCreditCards>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
        }
    }
}
