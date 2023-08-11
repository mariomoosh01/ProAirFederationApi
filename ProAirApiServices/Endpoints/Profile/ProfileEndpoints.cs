using static ProAirApiServices.WrapperEngine.EndpointAuthenticationDeclaration;
using ProAirApiServices.WrapperEngine.Framework;
using ProAirApiServices.DataLayer.DataServices;

namespace ProAirApiServices.Endpoints.Profile
{
    public class ProfileEndpoints : BaseEndpoint, IRequest
    {
        #region Constants

        private const string ROUTE = "/profile";
        private readonly ProfileServices profileServices;

        #endregion

        public ProfileEndpoints(ProfileServices profileServices)
        {
            this.profileServices = profileServices;
        }

        public override void RegisterAnonymous(WebApplication app)
        {
            Anonymous(
                app.MapGet($"{ROUTE}/getstates", () =>
                {
                    var states = profileServices.GetStates();

                    return Results.Ok(states);
                }),
                app.MapGet($"{ROUTE}/getCountries", () =>
                {
                    var countries = profileServices.GetCountries();

                    return Results.Ok(countries);
                })
            );
        }
    }
}
