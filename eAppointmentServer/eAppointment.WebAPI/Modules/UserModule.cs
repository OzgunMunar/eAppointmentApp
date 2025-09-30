using eAppointment.Application.Features.Users.UpdateUser;
using eAppointmentServer.Application.Features.Users.CreateUser;
using eAppointmentServer.Application.Features.Users.DeleteUser;
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

        group.MapPost(
            string.Empty, async (
                ISender sender,
                UserCreateCommand request,
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
            UserUpdateCommand request,
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
                var command = new UserDeleteCommand(id);
                var response = await sender.Send(command, cancellationToken);
                return response.IsSuccessful
                    ? Results.Ok(response)
                    : Results.InternalServerError(response);
            })
            .Produces<Result<string>>();

    }

}