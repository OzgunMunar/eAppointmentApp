using eAppointment.Application.Doctors.CreateDoctor;
using eAppointment.Application.Doctors.UpdateDoctor;
using Microsoft.AspNetCore.Mvc;
using TS.MediatR;
using TS.Result;

namespace eAppointment.WebAPI.Modules;

public static class DoctorModule
{
    public static void RegisterDoctorModuleRootes(this IEndpointRouteBuilder endpointRouteBuilder)
    {

        RouteGroupBuilder group = endpointRouteBuilder.MapGroup("/api/doctors").WithTags("Doctors")
            .RequireAuthorization();
            
        group.MapPost(
            string.Empty, async (
                ISender sender,
                DoctorCreateCommand request,
                CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful
                    ? Results.Ok(response)
                    : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

        group.MapPut("{id:guid}", async (
            [FromServices] ISender sender,
            Guid id,
            DoctorUpdateCommand request,
            CancellationToken cancellationToken) =>
            {

                var command = request with { Id = id };

                var response = await sender.Send(command, cancellationToken);
                return response.IsSuccessful
                    ? Results.Ok(response)
                    : Results.InternalServerError(response);

            })
            .Produces<Result<string>>();

        group.MapDelete("{id:guid}", async (
            [FromServices] ISender sender,
            Guid id,
            CancellationToken cancellationToken) =>
            {
                var command = new DoctorDeleteCommand(id);
                var response = await sender.Send(command, cancellationToken);
                return response.IsSuccessful
                    ? Results.Ok(response)
                    : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

    }

}