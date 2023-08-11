using AutoMapper;
using ProAirApiServices.DataLayer.Core.Mapper;
using ProAirApiServices.DataLayer.DataAccess.Core;
using ProAirApiServices.DataLayer.DataAccess.Entities;
using ProAirApiServices.DataLayer.DataAccess.Repositories;
using ProAirApiServices.DataLayer.Models.Dto.Profile;

namespace ProAirApiServices.DataLayer.DataServices
{
    public class ProfileServices
    {
        private readonly IRepository<States> _statesRepo;
        private readonly IRepository<Country> _countriesRepo;
        private readonly IMapper mapper = ServicesMapper.Mapper;

        public ProfileServices(ProAirDbContext dbContext)
        {
            _statesRepo = new Repository<States>(dbContext);
            _countriesRepo = new Repository<Country>(dbContext);
        }

        public List<CountryDto> GetCountries() => mapper.Map<List<CountryDto>>(_countriesRepo.GetAll());

        public List<StatesDto> GetStates()
        {
            var list = _statesRepo.GetAll().OrderBy(o=>o.StateId);

            return mapper.Map<List<StatesDto>>(list);
        }
    }
}
