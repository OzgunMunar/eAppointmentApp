using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Patients.PatientDelete;

internal sealed class PatientDeleteCommandHandler(
    IPatientRepository patientRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<PatientDeleteCommand, Result<string>>
{
    public async Task<Result<string>> Handle(PatientDeleteCommand request, CancellationToken cancellationToken)
    {

        Patient? patient = await patientRepository.FirstAsync(p => p.Id == request.Id,cancellationToken);

        if (patient == null)
        {
            return Result<string>.Failure("Patient not found.");
        }

        patient.IsActive = false;
        patient.IsDeleted = true;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Patient deleted successfully.");

    }
}