using AutoMapper;
using ProAirApiServices.DataLayer.Core.Mapper.MapperProfiles;

namespace ProAirApiServices.DataLayer.Core.Mapper
{
    public static class ServicesMapper
    {
        public static IMapper Mapper = null!;

        public static void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProfileServicesProfile>();
            });

            Mapper = config.CreateMapper();
        }
    }
}
