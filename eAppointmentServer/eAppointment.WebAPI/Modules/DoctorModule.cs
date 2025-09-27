using eAppointment.Application.Doctors.CreateDoctor;
using eAppointment.Domain.Entities;
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

        // group.MapGet(string.Empty, async (ISender sender, Guid UserId, CancellationToken cancellationToken) =>
        // {
        //     var response = await sender.Send(new DoctorGetQuery(UserId), cancellationToken);
        //     return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
        // })
        // .Produces<Result<Doctor>>();

    }
}