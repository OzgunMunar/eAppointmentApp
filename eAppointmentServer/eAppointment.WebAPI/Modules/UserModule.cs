using eAppointment.Application.Doctors.CreateDoctor;
using eAppointment.Application.Doctors.UpdateDoctor;
using Microsoft.AspNetCore.Mvc;
using TS.MediatR;
using TS.Result;

namespace eAppointment.WebAPI.Modules;

public static class UserModule
{
    public static void RegisterUserModuleRootes(this IEndpointRouteBuilder endpointRouteBuilder)
    {

        RouteGroupBuilder group = endpointRouteBuilder.MapGroup("/api/users").WithTags("Users")
            .RequireAuthorization();
            
    }

}