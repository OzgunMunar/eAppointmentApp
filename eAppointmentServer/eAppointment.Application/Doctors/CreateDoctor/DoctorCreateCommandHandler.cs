using eAppointment.Domain.CommonRecords;
using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using eAppointmentServer.Domain.Enums;
using GenericRepository;
using Mapster;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Doctors.CreateDoctor;

internal sealed class DoctorCreateCommandHandler(
    IDoctorRepository doctorRepository, IUnitOfWork unitOfWork
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

        // Doctor doctor = request.Adapt<Doctor>();

        var doctor = new Doctor
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            IdentityNumber = request.IdentityNumber,
            Department = DepartmentEnum.FromValue(request.Department),
            Address = new Address
            {
                Street = request.Street,
                City = request.City,
                Country = request.Country
            },
            PersonalInformation = new PersonalInformation
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            },
            IsActive = true
        };

        doctorRepository.Add(doctor);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Doctor is saved successfully.");

    }
}