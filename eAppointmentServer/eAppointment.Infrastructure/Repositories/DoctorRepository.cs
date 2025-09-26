using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using eAppointment.Infrastructure.Context;
using GenericRepository;

namespace eAppointment.Infrastructure.Repositories;

public sealed class DoctorRepository(ApplicationDbContext context) : Repository<Doctor, ApplicationDbContext>(context), IDoctorRepository
{
}