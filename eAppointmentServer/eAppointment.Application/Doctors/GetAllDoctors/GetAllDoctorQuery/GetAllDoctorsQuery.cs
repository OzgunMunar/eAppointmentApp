using TS.MediatR;

namespace eAppointment.Application.Doctors.GetAllDoctors.GetAllDoctorQuery;
public sealed record GetAllDoctorsQuery()
: IRequest<IQueryable<GetAllDoctorsQueryResponse>>;
