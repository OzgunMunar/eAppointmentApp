using eAppointment.Features.Roles.RoleSync;
using TS.MediatR;
using TS.Result;

namespace eAppointment.WebAPI.Modules;

public static class RolesModule
{
    public static void RegisterRolesModuleRootes(this IEndpointRouteBuilder endpointRouteBuilder)
    {

        RouteGroupBuilder group = endpointRouteBuilder.MapGroup("/api/RoleSync").WithTags("RoleSync");
            
        group.MapPost(
            string.Empty, async (
                ISender sender,
                RoleSyncCommand request,
                CancellationToken cancellationToken) =>
            {
                var response = await sender.Send(request, cancellationToken);
                return response.IsSuccessful
                    ? Results.Ok(response)
                    : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();
    }

}