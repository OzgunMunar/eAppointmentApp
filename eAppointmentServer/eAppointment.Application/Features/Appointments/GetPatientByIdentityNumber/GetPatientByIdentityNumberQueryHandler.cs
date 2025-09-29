using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Appointments.GetPatientByIdentityNumber;

internal sealed class GetPatientByIdentityNumberQueryHandler(
    IPatientRepository patientRepository
) : IRequestHandler<GetPatientByIdentityNumberQuery, Result<Patient>>
{
    public async Task<Result<Patient>> Handle(GetPatientByIdentityNumberQuery request, CancellationToken cancellationToken)
    {

        Patient? patient = await patientRepository
                    .GetByExpressionAsync(
                        p => p.IdentityNumber == request.IdentityNumber
                        &&
                        p.IsActive == true
                        , cancellationToken);

        return Result<Patient>.Succeed(patient);

    }
}
