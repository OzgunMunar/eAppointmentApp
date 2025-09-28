using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Patients.PatientDelete;

public sealed record PatientDeleteCommand(

    Guid Id

): IRequest<Result<string>>;
