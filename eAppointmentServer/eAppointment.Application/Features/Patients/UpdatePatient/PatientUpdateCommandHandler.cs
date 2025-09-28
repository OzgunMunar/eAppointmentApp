using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using FluentValidation;
using GenericRepository;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Patients.PatientUpdate;

internal sealed class PatientUpdateCommandHandler(
    IPatientRepository patientRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<PatientUpdateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(PatientUpdateCommand request, CancellationToken cancellationToken)
    {

        Patient? searchPatient = await (
            from patient in patientRepository.GetAll().Where(p => p.IsActive == true && p.IsDeleted == false)
            where patient.Id == request.Id
            select patient
        ).FirstOrDefaultAsync(cancellationToken);

        if (searchPatient == null)
        {
            return Result<string>.Failure("Patient record not found.");
        }

        mapper.Map(request, searchPatient);

        patientRepository.Update(searchPatient);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Patient is updated.");

    }
}