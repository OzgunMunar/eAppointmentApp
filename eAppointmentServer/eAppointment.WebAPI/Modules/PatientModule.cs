using eAppointment.Application.Features.Patients.Create;
using eAppointment.Application.Features.Patients.PatientDelete;
using eAppointment.Application.Features.Patients.PatientUpdate;
using Microsoft.AspNetCore.Mvc;
using TS.MediatR;
using TS.Result;

namespace eAppointment.WebAPI.Modules;

public static class PatientModule
{
    public static void RegisterPatientModuleRootes(this IEndpointRouteBuilder endpointRouteBuilder)
    {

        RouteGroupBuilder group = endpointRouteBuilder.MapGroup("/api/patients").WithTags("Patients")
        .RequireAuthorization();
        
        group.MapPost(
            string.Empty, async (
                ISender sender,
                PatientCreateCommand request,
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
            PatientUpdateCommand request,
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
                var command = new PatientDeleteCommand(id);
                var response = await sender.Send(command, cancellationToken);
                return response.IsSuccessful
                    ? Results.Ok(response)
                    : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

    }

}