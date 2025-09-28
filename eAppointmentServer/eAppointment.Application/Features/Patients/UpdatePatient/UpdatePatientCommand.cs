using System.Reflection;
using Mapster;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Patients.UpdatePatient;

public sealed record UpdatePatientCommand(

    Guid Id,
    string FirstName,
    string LastName,
    string IdentityNumber,
    string Country,
    string City,
    string Street,
    string Email,
    string PhoneNumber

): IRequest<Result<string>>;
