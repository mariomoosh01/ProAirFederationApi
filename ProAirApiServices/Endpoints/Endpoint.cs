using ProAirApiServices.WrapperEngine.Framework;

namespace ProAirApiServices.Endpoints
{
    public class Endpoint : IRequest
    {
        public virtual void RegisterAnonymous(WebApplication app){ }

        public void RegisterAuthorized(WebApplication app) { }

        public virtual void RegisterBasic(WebApplication app) { }

        public virtual void RegisterElite(WebApplication app) { }

        public virtual void RegisterPremium(WebApplication app) { }
    }
}
