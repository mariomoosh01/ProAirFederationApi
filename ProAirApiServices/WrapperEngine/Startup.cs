using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProAirApiServices.Core;
using ProAirApiServices.DataLayer.Core.PaymentEngine;
using ProAirApiServices.DataLayer.Core.Security;
using ProAirApiServices.DataLayer.DataAccess.Core;
using ProAirApiServices.DataLayer.DataServices;
using ProAirApiServices.DataLayer.DataServices.Contracts;
using ProAirApiServices.DataLayer.DataServices.MemberServices;
using ProAirApiServices.Endpoints.Security;
using ProAirApiServices.WrapperEngine.Framework;
using System.Security.Claims;
using System.Text;

namespace ProAirApiServices.WrapperEngine
{
    
    public static class Startup
    {
        public static void AddCoreServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<ProAirDbContext>(options=>
            {
                options.UseSqlServer(config.GetConnectionString("ProAir"));
            });
            services.AddScoped<TokenServices>();
            services.AddScoped<TextEncryptorEngine>();
            services.AddScoped<IMemberServices, MemberServices>();
            services.AddScoped<Login>();
            services.AddTransient<CreditCardStrategy>();
            services.AddScoped<ProfileServices>();
        }

        public static void AddApiServices(this IServiceCollection services, IConfiguration config)
        {          
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("BasicLevel", authBuilder =>
                {
                    authBuilder.RequireRole("Basic");
                });
            });
        }

        public static IServiceCollection AddProAirServices<T>(this IServiceCollection services)
        {
            var targetServices = typeof(T).Assembly.GetTypes()
                .Select(t => (t, t.BaseType))
                .Where((tuple) => tuple.BaseType != null)
                .Where((tuple) => typeof(IRequest).IsAssignableFrom(tuple.t));

            foreach (var service in targetServices)
            {
                services.AddScoped(service.Item1);
            }

            return services;
        }

        //public static IServiceCollection AddValidation<T>(this IServiceCollection services)
        //{ 
        //    var validators = typeof(T).Assembly.GetTypes()
        //        .Select(t=> (t,t.BaseType))
        //        .Where((tuple) => tuple.BaseType != null)
        //        .Where((tuple) => tuple.BaseType.IsGenericType && tuple.BaseType.IsAbstract && tuple.BaseType.GetGenericTypeDefinition().IsEquivalentTo(typeof(AbstractValidator<>)))
        //        .Select((tuple) => (tuple.t, tuple.BaseType.GetGenericArguments()[0]));

        //    foreach (var validator in validators)
        //    {
        //        var validatorInterfaceType = typeof(IValidator<>).MakeGenericType(validator.Item1);

        //        services.AddTransient(validatorInterfaceType, validator.Item1);
        //    }

        //    return services;
        //}
    }
}