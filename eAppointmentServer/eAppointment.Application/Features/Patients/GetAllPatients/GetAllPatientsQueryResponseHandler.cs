using eAppointment.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using TS.MediatR;

namespace eAppointmentServer.Application.Features.Patients.GetAllPatients;

internal sealed class GetAllPatientsQueryResponseHandler(
    IPatientRepository patientRepository
) : IRequestHandler<GetAllPatientsQuery, IQueryable<GetAllPatientsQueryResponse>>
{
    public async Task<IQueryable<GetAllPatientsQueryResponse>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
    {

        var patients = await
            (from patient in patientRepository.GetAll()
             where patient.IsActive == true && patient.IsDeleted == false

             select new GetAllPatientsQueryResponse
             {
                 Id = patient.Id,
                 FirstName = patient.FirstName,
                 LastName = patient.LastName,
                 IdentityNumber = patient.IdentityNumber,
                 Country = patient.Country,
                 City = patient.City,
                 Street = patient.Street,
                 Email = patient.Email,
                 PhoneNumber = patient.PhoneNumber
             })
            .ToListAsync(cancellationToken);

        return patients.AsQueryable();

    }
}