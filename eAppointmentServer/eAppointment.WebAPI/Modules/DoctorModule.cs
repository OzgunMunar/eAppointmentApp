using eAppointment.Application.Doctors.CreateDoctor;
using eAppointment.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using TS.MediatR;
using TS.Result;

namespace eAppointment.WebAPI.Modules;

public static class DoctorModule
{
    public static void RegisterDoctorModuleRootes(this IEndpointRouteBuilder endpointRouteBuilder)
    {

        RouteGroupBuilder group = endpointRouteBuilder.MapGroup("/api/doctors").WithTags("Doctors");
        // .RequireAuthorization();
        group.MapPost(string.Empty, async (ISender sender, DoctorCreateCommand request, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(request, cancellationToken);
            return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
        })
        .Produces<Result<string>>();

        group.MapDelete("{id:guid}", async ([FromServices] ISender sender, Guid id, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new DoctorDeleteCommand(id), cancellationToken);
            return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
        });


    }
}