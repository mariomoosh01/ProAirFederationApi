using Microsoft.AspNetCore.Mvc;
using ProAirApiServices.Core;
using ProAirApiServices.DataLayer.Core.PaymentEngine;
using ProAirApiServices.DataLayer.Core.PaymentEngine.Models;
using ProAirApiServices.DataLayer.Models.Dto.Login;
using static ProAirApiServices.WrapperEngine.EndpointAuthenticationDeclaration;
using ProAirApiServices.WrapperEngine.Framework;
using ProAirApiServices.DataLayer.DataServices.Contracts;
using ProAirApiServices.DataLayer.Models.Post;
using ProAirApiServices.DataLayer.Models.Dto.Members;
using ProAirApiServices.DataLayer.Core.Security;
using Serilog;

namespace ProAirApiServices.Endpoints.Membership
{
    public class MembershipEndpoints: BaseEndpoint, IRequest
    {
        #region Constants
        private const string ROUTE = "/membership";
        private readonly IConfiguration config;
        private const int FREE_MEMBERSHIP_YEARS = 1;
        private const string TAG = "MembershipAPI";
        #endregion

        #region Fields
        private IPaymentService<CreditCardModel>? _ccProcessor;
        private IMemberServices _memberServices;
        private readonly TextEncryptorEngine _encryptor;
        private static void LogError(string message) => Log.Error($"{TAG}: {message}");
        #endregion

        public MembershipEndpoints(IConfiguration config, IMemberServices memberServices, TextEncryptorEngine encryptor)
        {
            this.config = config;
            _memberServices = memberServices;
            _encryptor = encryptor;
        }

        public override void RegisterAnonymous(WebApplication app)
        {
            Anonymous(
            #region POST
               app.MapPost($"{ROUTE}/memberRegistration", ([FromBody] MemberRegistrationPost model) =>
               {
                   try
                   {
                       var memberModel = new MemberDto 
                       {
                           Email = model.Email,
                           DisplayName = model.DisplayName,
                           FirstName = model.FirstName,
                           LastName = model.LastName,
                           CountryId = model.CountryId,
                           Address = model.Address,
                           City = model.City,
                           State = model.State,
                           Zip = model?.Zip == null ? (short)0 : (short)model.Zip,
                           MembershipLevel = model.Level,
                           ExpirationDate = DateTime.Now.AddYears(FREE_MEMBERSHIP_YEARS),                           
                           Password = Convert.ToBase64String(_encryptor.EncryptPassword(model.Password)),
                           DateCreated = DateTime.Now,
                           LastUpdated = DateTime.Now
                       };
                       //save account info
                       _memberServices.SaveMember(memberModel);
                       //save card info
                       if (model.StoreCreditCard)
                       {
                           var memberCcModel = new MembersCreditCardsDto
                           {
                               MemberId = model.Email,
                               CardNumber = model?.CreditCardModel?.CardNumber??"",
                               FirstName = model?.CreditCardModel?.FirstName??"",
                               LastName = model?.CreditCardModel?.LastName??"",
                               ExpirationDate = model?.CreditCardModel?.ExpirationDate??"",
                               CvvCode = model?.CreditCardModel?.CardCode??0,
                               UseProfileAddress = model?.CreditCardModel?.UseProfileAddress??false,
                               Address = model?.CreditCardModel?.Address??"",
                               City = model?.CreditCardModel?.City??"",
                               StateId = model?.CreditCardModel?.State??-1,
                               Zip = model?.Zip == null ? (short)0 : (short)model.Zip,
                               DateAdded = DateTime.Now,
                               DateModified = DateTime.Now
                           };
                           _memberServices.SaveMemberCreditCard(memberCcModel); 
                       }

                       return Results.Ok(true);
                   }
                   catch (Exception ex)
                   {
                               LogError($"Message: {ex.Message} Inner Exception: {ex?.InnerException?.Message}");
                       return Results.BadRequest("Error found while saving member data.");
                   }
               }),
               app.MapPost($"{ROUTE}/members", ([FromBody] MemberDto model) =>
               {                   
                   return Results.Ok("");
               }),
               app.MapPost($"{ROUTE}/donation/cc", ([FromBody] CreditCardModel model) => {
                   _ccProcessor = (IPaymentService<CreditCardModel>)PaymentServices.GetPaymentOption(PaymentMethods.CreditCard);

                   var transaction = _ccProcessor.Checkout(model);

                   return Results.Ok(transaction);
               }),
            #endregion
            #region GET
               app.MapGet($"{ROUTE}/getDisplayName",([FromQueryAttribute] string displayName) =>
               {
                   try
                   {
                       var dp = _memberServices.GetDisplayName(displayName);

                       return Results.Ok(dp);
                   }
                   catch (Exception ex)
                   {
                       LogError(ex.Message);
                       return Results.BadRequest("Error found while getting display name.");
                   }
               })
            #endregion
           );
        }

        public override void RegisterAuthorized(WebApplication app)
        {
            Authorized(
                app.MapGet($"{ROUTE}/getProfile", ([FromQuery] string email) => {
                    var user = _memberServices.GetMemberProfile(email);
                    return Results.Ok(user);
                })
            );
        }   
    }
}
