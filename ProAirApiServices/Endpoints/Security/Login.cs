using Microsoft.AspNetCore.Mvc;
using ProAirApiServices.Core;
using ProAirApiServices.DataLayer.Core.Security;
using ProAirApiServices.DataLayer.DataServices.Contracts;
using ProAirApiServices.DataLayer.Models.Dto.Login;
using ProAirApiServices.Models;
using ProAirApiServices.WrapperEngine;
using static ProAirApiServices.WrapperEngine.EndpointAuthenticationDeclaration;

namespace ProAirApiServices.Endpoints.Security
{
    public class Login: Endpoint
    {
        #region Constants
        private const string ROUTE = "/login";
        private readonly IMemberServices _memberServices;
        private readonly TokenServices _tokenServices;
        #endregion

        #region Fields

        private readonly TextEncryptorEngine _encryptor;

        #endregion

        public Login(IMemberServices memberServices, TokenServices tokenServices, TextEncryptorEngine encryptor)
        {
            _memberServices = memberServices;
            _tokenServices = tokenServices;
            _encryptor = encryptor;
        }

        public override void RegisterAnonymous(WebApplication app)
        {
            Anonymous(
                app.MapPost($"{ROUTE}/createToken", ([FromBody] MemberDto model) =>
                {
                    var validUser = _memberServices.AuthenticateMember(model);

                    if (!validUser) return Results.Unauthorized();

                    var token = _tokenServices.CreateToken(model);

                    return Results.Ok(token);
                }),
                app.MapPost($"{ROUTE}/authenticate", ([FromBody] LoginModel loginModel) => {
                    var model = new MemberDto { 
                        Email = loginModel.Email, 
                        Password = loginModel.Password
                     };
                    var validUser = _memberServices.AuthenticateMember(model);

                    if (!validUser) return Results.Unauthorized();

                    var user = _memberServices.GetMemberProfile(model.Email);
                    var token = _tokenServices.CreateToken(model);

                    return Results.Ok(new { Name = $"{user.FirstName} {user.LastName}", DisplayName = user.DisplayName, Token = token});
                })
            ); ;
        }
    }
}
