using eAppointment.Application.Doctors.GetAllDoctors.GetAllDoctorQuery;
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
// [Authorize]
[AllowAnonymous]
public class AppODataController(ISender sender) : ODataController
{

    public static IEdmModel GetEdmModel()
    {

        ODataConventionModelBuilder builder = new();

        builder.EnableLowerCamelCase();
        builder.EntitySet<GetAllDoctorsQueryResponse>("doctors");

        return builder.GetEdmModel();

    }

    [HttpGet("doctors")]
    public async Task<IQueryable<GetAllDoctorsQueryResponse>> GetAllDoctors(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetAllDoctorsQuery(), cancellationToken);
        return response;
    }
}