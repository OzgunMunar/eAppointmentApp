using eAppointment.Application.Doctors.GetAllDoctors.GetAllDoctorQuery;
using eAppointmentServer.Application.Features.Patients.GetAllPatients;
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

}