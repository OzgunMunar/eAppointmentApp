using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using eAppointment.Infrastructure.Context;
using GenericRepository;

namespace eAppointment.Infrastructure.Repositories;

public sealed class AppointmentRepository(ApplicationDbContext context) : Repository<Appointment, ApplicationDbContext>(context), IAppointmentRepository
{
}