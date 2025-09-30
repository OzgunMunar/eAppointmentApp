using eAppointment.Application.Doctors.GetAllDoctors.GetAllDoctorQuery;
using eAppointment.Application.Features.Roles.GetAllRoles;
using eAppointment.Domain.Users;
using eAppointmentServer.Application.Features.Patients.GetAllPatients;
using eAppointmentServer.Application.Features.Users.GetAllUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using TS.MediatR;

namespace eAppointment.WebAPI.Controllers;

[Route("odata")]
[ApiController]
[EnableQuery]
[Authorize]
public class AppODataController(ISender sender) : ODataController
{

    public static IEdmModel GetEdmModel()
    {

        ODataConventionModelBuilder builder = new();

        builder.EnableLowerCamelCase();

        builder.EntitySet<GetAllDoctorsQueryResponse>("doctors");
        builder.EntitySet<GetAllPatientsQueryResponse>("patients");
        builder.EntitySet<GetAllUsersQueryResponse>("users");
        builder.EntitySet<AppRole>("roles");

        return builder.GetEdmModel();

    }

    [HttpGet("doctors")]
    public async Task<IQueryable<GetAllDoctorsQueryResponse>> GetAllDoctors(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetAllDoctorsQuery(), cancellationToken);
        return response;
    }

    [HttpGet("patients")]
    public async Task<IQueryable<GetAllPatientsQueryResponse>> GetAllPatients(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetAllPatientsQuery(), cancellationToken);
        return response;
    }

    [HttpGet("roles")]
    public async Task<IQueryable<AppRole>> GetAllRoles(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetAllRolesQuery(), cancellationToken);
        return response;
    }

    [HttpGet("users")]
    public async Task<IQueryable<GetAllUsersQueryResponse>> GetAllUsers(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetAllUsersQuery(), cancellationToken);
        return response;
    }

}