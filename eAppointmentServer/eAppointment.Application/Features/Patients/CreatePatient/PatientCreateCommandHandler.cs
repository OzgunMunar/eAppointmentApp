using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using GenericRepository;
using Mapster;
using Microsoft.EntityFrameworkCore;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Patients.Create;

internal sealed class PatientCreateCommandHandler(
    IPatientRepository patientRepository,
    IUnitOfWork unitOfWork
)
: IRequestHandler<PatientCreateCommand, Result<string>>
{

    public async Task<Result<string>> Handle(PatientCreateCommand request, CancellationToken cancellationToken)
    {

        Patient? searchPatient = await (

            from patient in patientRepository.GetAll().Where(p => p.IsActive == true && p.IsDeleted == false)
            where patient.IdentityNumber == request.IdentityNumber
            select patient
            
        ).FirstOrDefaultAsync(cancellationToken);

        if (searchPatient != null)
        {
            return Result<string>.Failure("There is already a doctor with the same Identity Number.");
        }

        Patient newPatient = request.Adapt<Patient>();

        patientRepository.Add(newPatient);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Patient record created.");

    }

}