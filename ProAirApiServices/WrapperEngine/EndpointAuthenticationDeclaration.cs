namespace ProAirApiServices.WrapperEngine
{
    public class EndpointAuthenticationDeclaration
    {
        public static void Anonymous(params IEndpointConventionBuilder[] ecb)
        {
            foreach (var endPoint in ecb)
            {
                endPoint.AllowAnonymous();
            }
        }

        public static void BasicLevel(params IEndpointConventionBuilder[] ecb)
        {
            foreach (var endPoint in ecb)
            {
                endPoint.RequireAuthorization("BasicLevel");
            }
        }

        public static void PremiumLevel(params IEndpointConventionBuilder[] ecb)
        {
            foreach (var endPoint in ecb)
            {
                endPoint.RequireAuthorization("Premium");
            }
        }

        public static void Authorized(params IEndpointConventionBuilder[] ecb)
        {
            foreach(var endpoint in ecb)
            {
                endpoint.RequireAuthorization();
            }
        }
    }
}
