using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using eAppointmentServer.Domain.Enums;
using GenericRepository;
using Mapster;
using MapsterMapper;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Doctors.UpdateDoctor;

internal sealed class DoctorUpdateCommandHandler(
    IDoctorRepository doctorRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<DoctorUpdateCommand, Result<string>>
{

    async Task<Result<string>> IRequestHandler<DoctorUpdateCommand, Result<string>>.Handle(DoctorUpdateCommand request, CancellationToken cancellationToken)
    {

        Doctor? doctor = await doctorRepository.FirstAsync(doctor =>
            doctor.Id == request.Id
            &&
            doctor.IsActive == true
            &&
            doctor.IsDeleted == false,
            cancellationToken);

        if (doctor == null)
        {
            return Result<string>.Failure("Doctor is already deleted by another user.");
        }

        var config = new TypeAdapterConfig();
        config.NewConfig<DoctorUpdateCommand, Doctor>()
              .Map(dest => dest.Department, src => DepartmentEnum.FromValue(src.Department));

        mapper = new Mapper(config);
        mapper.Map(request, doctor);

        doctorRepository.Update(doctor);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Doctor is updated successfully.");

    }
}