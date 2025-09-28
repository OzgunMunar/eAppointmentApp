using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using eAppointmentServer.Domain.Enums;
using GenericRepository;
using Mapster;
using MapsterMapper;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Doctors.CreateDoctor;

internal sealed class DoctorCreateCommandHandler(
    IDoctorRepository doctorRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<DoctorCreateCommand, Result<string>>
{

    async Task<Result<string>> IRequestHandler<DoctorCreateCommand, Result<string>>.Handle(DoctorCreateCommand request, CancellationToken cancellationToken)
    {

        bool searchDoctor = await doctorRepository.AnyAsync(doctor =>
            doctor.IdentityNumber == request.IdentityNumber
            &&
            doctor.IsActive == true,
            cancellationToken);

        if (searchDoctor)
        {
            return Result<string>.Failure("Doctor with same Identity Number already exist.");
        }

        var config = new TypeAdapterConfig();
        config.NewConfig<DoctorCreateCommand, Doctor>()
              .Map(dest => dest.Department, src => DepartmentEnum.FromValue(src.Department));

        Doctor doctor = request.Adapt<Doctor>(config);

        doctorRepository.Add(doctor);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Doctor is saved successfully.");

    }
}