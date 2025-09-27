using eAppointment.Application;
using eAppointment.Infrastructure;
using eAppointment.WebAPI;
using eAppointment.WebAPI.Controllers;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.RateLimiting;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Identity;
using eAppointment.Domain.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCompression(opt =>
{
    opt.EnableForHttps = true;
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors();
builder.Services.AddOpenApi();

builder.Services.AddControllers().AddOData(options =>
{

    options
        .EnableQueryFeatures()
        .AddRouteComponents("odata", AppODataController.GetEdmModel());

});
builder.Services.AddHttpContextAccessor();

builder.Services.AddRateLimiter(x =>
    x.AddFixedWindowLimiter("fixed", cfg =>
    {
        cfg.QueueLimit = 100;
        cfg.Window = TimeSpan.FromSeconds(1);
        cfg.PermitLimit = 100;
        cfg.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    }));

builder.Services.AddExceptionHandler<ExceptionHandler>().AddProblemDetails();

var app = builder.Build();

// app.UseHttpsRedirection();

app.UseCors(x => x
    .WithOrigins("http://localhost:4200")
    .AllowAnyHeader()
    .AllowCredentials()
    .AllowAnyMethod()
    .SetIsOriginAllowed(t => true));

app.UseResponseCompression();

app.UseExceptionHandler();

app.MapOpenApi();

app.MapScalarApiReference();

app.MapControllers();
// .RequireRateLimiting("fixed");
// .RequireAuthorization();

app.RegisterRoutes();

// app.UseAuthentication();
// app.UseAuthorization();

ExtensionsMiddleware.CreateFirstUser(app);

Helper.CreateUserAsync(app).Wait();

app.Run();