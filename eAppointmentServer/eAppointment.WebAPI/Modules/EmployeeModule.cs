using eAppointment.Application.Employees;
using eAppointment.Domain.Employees;
using TS.MediatR;
using TS.Result;

namespace eAppointment.WebAPI.Modules;
public static class EmployeeModule
{
    public static void RegisterEmployeeRoutes(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        RouteGroupBuilder group = endpointRouteBuilder.MapGroup("/employees").WithTags("Employees").RequireAuthorization();

        group.MapPost(string.Empty, async (ISender sender, EmployeeCreateCommand request, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(request, cancellationToken);
            return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
        })
        .Produces<Result<string>>();

        group.MapGet(string.Empty, async (ISender sender, Guid UserId, CancellationToken cancellationToken) =>
        {
            var response = await sender.Send(new EmployeeGetQuery(UserId), cancellationToken);
            return response.IsSuccessful ? Results.Ok(response) : Results.InternalServerError(response);
        })
        .Produces<Result<Employee>>();

    }
}