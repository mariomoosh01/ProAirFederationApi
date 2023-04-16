namespace ProAirApiServices.Endpoints
{
    public class BaseEndpoint
    {
        public virtual void RegisterAnonymous(WebApplication app)
        {
            
        }

        public virtual void RegisterAuthorized(WebApplication app)
        {
        }

        public virtual void RegisterBasic(WebApplication app)
        {

        }

        public virtual void RegisterPremium(WebApplication app)
        {

        }
    }
}
