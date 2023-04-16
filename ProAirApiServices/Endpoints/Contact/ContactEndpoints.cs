using Microsoft.AspNetCore.Mvc;
using ProAirApiServices.DataLayer.Core.DataTransfer;
using ProAirApiServices.DataLayer.Models.Post;
using ProAirApiServices.WrapperEngine.Framework;
using Serilog;

namespace ProAirApiServices.Endpoints.Emails
{
    public class ContactEndpoints: BaseEndpoint, IRequest
    {
        public override void RegisterAnonymous(WebApplication app)
        {
            app.MapPost($"contact/inquiry", ([FromBody] ContactQuestionPost model) => {
                try
                {
                    var body = $"{model.Name} <br /> Phone: {model.Phone} <br /> Email: {model.Email} <br /> Question: <br /> {model.Message}";
                    SendEmail.SendToInfo(model.Subject, body);

                    return Results.Ok(true);
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    return Results.BadRequest(false);
                }
            });   
        }
    }
}