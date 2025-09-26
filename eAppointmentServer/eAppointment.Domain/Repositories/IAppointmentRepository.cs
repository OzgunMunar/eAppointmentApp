using eAppointment.Domain.Entities;
using GenericRepository;

namespace eAppointment.Domain.Repositories;
public interface IAppointmentRepository : IRepository<Appointment>
{
    
}