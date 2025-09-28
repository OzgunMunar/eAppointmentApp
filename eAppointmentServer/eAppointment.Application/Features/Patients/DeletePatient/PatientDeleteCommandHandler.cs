using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using GenericRepository;
using Microsoft.EntityFrameworkCore;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Patients.DeletePatient;

internal sealed class PatientDeleteCommandHandler(
    IPatientRepository patientRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<PatientDeleteCommand, Result<string>>
{
    public async Task<Result<string>> Handle(PatientDeleteCommand request, CancellationToken cancellationToken)
    {

        Patient? searchPatient = await (
            from patient in patientRepository.GetAll().Where(p => p.IsActive == true && p.IsDeleted == false)
            where patient.Id == request.Id
            select patient
        ).FirstOrDefaultAsync(cancellationToken);

        if (searchPatient == null)
        {
            return Result<string>.Failure("Patient not found.");
        }

        searchPatient.IsActive = false;
        searchPatient.IsDeleted = true;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Patient deleted successfully.");

    }
}