using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using GenericRepository;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Doctors.CreateDoctor;

internal sealed class DoctorDeleteCommandHandler(
    IDoctorRepository doctorRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<DoctorDeleteCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DoctorDeleteCommand request, CancellationToken cancellationToken)
    {

        Doctor? doctor = await doctorRepository.FirstAsync(doctor => doctor.Id == request.Id, cancellationToken);

        if (doctor is null)
        {
            return Result<string>.Failure("Doctor couldn't be found.");
        }

        doctor.IsDeleted = true;
        doctor.IsActive = false;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Doctor is deleted successfully");

    }

}