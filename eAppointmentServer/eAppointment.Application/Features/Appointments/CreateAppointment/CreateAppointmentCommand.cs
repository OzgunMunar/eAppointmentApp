using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using GenericRepository;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Appointments.CreateAppointment;

public sealed record CreateAppointmentCommand(

    string StartDate,
    string EndDate,
    Guid DoctorId,
    Guid? PatientId,
    string FirstName,
    string LastName,
    string IdentityNumber,
    string Country,
    string City,
    string Street,
    string Email,
    string PhoneNumber

) : IRequest<Result<string>>;

internal sealed class CreateAppointmentCommandHandler(
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork,
    IPatientRepository patientRepository
) : IRequestHandler<CreateAppointmentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {

        Patient patient = new();

        if (request.PatientId is null)
        {

            patient = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdentityNumber = request.IdentityNumber,
                Country = request.Country,
                City = request.City,
                Street = request.Street,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber

            };


            await patientRepository.AddAsync(patient, cancellationToken);

        }

        Appointment appointment = new()
        {
            DoctorId = request.DoctorId,
            PatientId = request.PatientId ?? patient.Id,
            StartDate = Convert.ToDateTime(request.StartDate),
            EndDate = Convert.ToDateTime(request.EndDate),
            IsCompleted = false
        };

        await appointmentRepository.AddAsync(appointment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Appointment created successfully.");

    }
}