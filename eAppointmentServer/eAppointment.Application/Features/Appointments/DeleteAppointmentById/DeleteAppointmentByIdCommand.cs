using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using GenericRepository;
using TS.MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.DeleteAppointmentById;

public sealed record DeleteAppointmentByIdCommand(

    Guid Id

): IRequest<Result<string>>;

internal sealed class DeleteAppointmentByIdCommandHandler(
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork
)
: IRequestHandler<DeleteAppointmentByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteAppointmentByIdCommand request, CancellationToken cancellationToken)
    {

        Appointment? appointment = await appointmentRepository
                            .FirstOrDefaultAsync(appo => appo.Id == request.Id, cancellationToken);

        if (appointment == null)
        {

            return Result<string>.Failure("Appointment could not found");

        }

        if (appointment.IsCompleted == true)
        {
            return Result<string>.Failure("Completed appointments could not be deleted.");
        }

        appointment.IsActive = false;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Appointment is deleted");

    }
    
}