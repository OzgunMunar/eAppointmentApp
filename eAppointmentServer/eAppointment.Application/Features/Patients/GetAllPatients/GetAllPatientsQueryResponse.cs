using eAppointment.Domain.Abstractions;
using eAppointment.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using TS.MediatR;

namespace eAppointmentServer.Application.Features.Patients.GetAllPatients;

public sealed class GetAllPatientsQueryResponse : EntityDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string IdentityNumber { get; set; } = default!;
    public string FullName => string.Join(" ", FirstName, LastName);
    public string Country { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}
