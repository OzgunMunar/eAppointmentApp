using eAppointment.Domain.Entities;
using eAppointment.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using TS.MediatR;
using TS.Result;

namespace eAppointment.Application.Features.Appointments.GetAllDoctorsByDepartment;


public sealed record GetAllDoctorsByDepartmentQuery(
    int DepartmentValue
):IRequest<Result<List<Doctor>>>;

internal sealed class GetAllDoctorsByDepartmentQueryHandler(
    IDoctorRepository doctorRepository
) : IRequestHandler<GetAllDoctorsByDepartmentQuery, Result<List<Doctor>>>
{
    public async Task<Result<List<Doctor>>> Handle(GetAllDoctorsByDepartmentQuery request, CancellationToken cancellationToken)
    {

        List<Doctor> doctors = await doctorRepository
                .Where(p =>
                    p.Department == request.DepartmentValue
                    &&
                    p.IsActive == true && p.IsDeleted == false
                    )
                    .OrderBy(p => p.FirstName)
                    .ToListAsync(cancellationToken);

        return Result<List<Doctor>>.Succeed(doctors);

    }
}