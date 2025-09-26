using eAppointment.Domain.Abstractions;
using TS.MediatR;

namespace eAppointment.Application.Doctors.GetAllDoctors.GetAllDoctorQuery;

public sealed class GetAllDoctorsQueryResponse : EntityDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => string.Join(" ", FirstName, LastName);
    public string IdentityNumber { get; set; } = default!;
    public int Department { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Street { get; set; }
}
