using eAppointment.Application.Features.Appointments.GetAllAppointmentsByDoctorId;
using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Appointments.GetAllAppointments;

internal sealed class GetAllAppointmentsQueryHandler(
    IAppointmentRepository appointmentRepository
) :
IRequestHandler<GetAllAppointmentsByDoctorIdQuery, Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>>
{
    public async Task<Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>> Handle(GetAllAppointmentsByDoctorIdQuery request, CancellationToken cancellationToken)
    {
        List<Appointment> appointments = await appointmentRepository
                    .Where(
                        p => p.DoctorId == request.DoctorId
                        &&
                        p.IsActive == true)
                    .Include(p => p.Patient)
                    .ToListAsync(cancellationToken);

        List<GetAllAppointmentsByDoctorIdQueryResponse> response =
            appointments.Select(s => new GetAllAppointmentsByDoctorIdQueryResponse(
                s.Id,
                s.StartDate,
                s.EndDate,
                s.Patient!.FullName,
                s.Patient
            ))
            .ToList();

        return response;

    }
}