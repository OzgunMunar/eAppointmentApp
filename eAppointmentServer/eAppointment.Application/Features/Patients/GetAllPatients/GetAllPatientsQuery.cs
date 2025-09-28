using TS.MediatR;

namespace eAppointmentServer.Application.Features.Patients.GetAllPatients;

public sealed record GetAllPatientsQuery():
    IRequest<IQueryable<GetAllPatientsQueryResponse>>;
