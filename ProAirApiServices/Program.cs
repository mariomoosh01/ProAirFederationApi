using ProAirApiServices.DataLayer.Core.Mapper;
using ProAirApiServices.DataLayer.Core.PaymentEngine;
using ProAirApiServices.WrapperEngine;
using ProAirApiServices.WrapperEngine.Framework;
using Serilog;

const string CORS_POLICY_NAME = "ProAirCorsPolicy";
string[] CORS_ALLOWED_METHODS = { "get", "post", "put" };

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()    
    .ReadFrom.Configuration(builder.Configuration)    
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Host.UseSerilog();
builder.Services.AddProAirServices<Program>();
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS_POLICY_NAME,
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .WithMethods(CORS_ALLOWED_METHODS);
        });
});
builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

PaymentServices.ServiceScope = app.Services.CreateScope();
ServicesMapper.Configure();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(CORS_POLICY_NAME);

var requestType = typeof(IRequest);
var types = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => requestType.IsAssignableFrom(p))
    .Where(p => p.BaseType != null);

using var scope = app.Services.CreateScope();


foreach (var t in types) 
{    
    var s = scope.ServiceProvider.GetService(t);

    ((IRequest)s).RegisterAnonymous(app);
    ((IRequest)s).RegisterBasic(app);
}


app.Run();