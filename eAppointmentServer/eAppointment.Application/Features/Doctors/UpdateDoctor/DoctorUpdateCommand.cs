using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Doctors.UpdateDoctor;    

public sealed record DoctorUpdateCommand
(
    Guid Id,
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
