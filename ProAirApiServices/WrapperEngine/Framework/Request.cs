namespace ProAirApiServices.WrapperEngine.Framework
{

    public interface IRequest
    {
        void RegisterAnonymous(WebApplication app);
        void RegisterBasic(WebApplication app);
        void RegisterPremium(WebApplication app);
        void RegisterAuthorized(WebApplication app);
    }    
}
