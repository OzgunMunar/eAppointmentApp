using eAppointmentServer.Domain.Enums;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Doctors.CreateDoctor;    

public sealed record DoctorCreateCommand
(
    string FirstName,
    string LastName,
    string IdentityNumber,
    string Country,
    string City,
    string Street,
    string Email,
    string PhoneNumber,
    int Department

) : IRequest<Result<string>>;
