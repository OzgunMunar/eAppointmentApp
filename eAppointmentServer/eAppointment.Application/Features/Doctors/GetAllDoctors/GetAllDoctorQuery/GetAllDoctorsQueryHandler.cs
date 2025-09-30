using eAppointment.Domain.Repositories;
using eAppointment.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.MediatR;

namespace eAppointment.Application.Doctors.GetAllDoctors.GetAllDoctorQuery;

internal sealed class GetAllDoctorsQueryHandler(

    IDoctorRepository doctorRepository,
    UserManager<AppUser> userManager

) : IRequestHandler<GetAllDoctorsQuery, IQueryable<GetAllDoctorsQueryResponse>>
{
    public async Task<IQueryable<GetAllDoctorsQueryResponse>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
    {

        var doctors = await doctorRepository
            .GetAll()
            .Where(doctor => doctor.IsActive == true && doctor.IsDeleted == false)
            .ToListAsync(cancellationToken);

        var response = await (

            from doctor in doctorRepository.GetAll()

            join created_user in userManager.Users.AsQueryable() on doctor.CreatedUserId equals created_user.Id

            join update_user in userManager.Users.AsQueryable() on doctor.UpdatedUserId equals update_user.Id into update_user
            from update_users in update_user.DefaultIfEmpty()

            orderby doctor.FirstName ascending, doctor.Department ascending

            where doctor.IsActive == true && doctor.IsDeleted == false

            select new GetAllDoctorsQueryResponse
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                IdentityNumber = doctor.IdentityNumber,
                Department = doctor.Department,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                City = doctor.City,
                Country = doctor.Country,
                Street = doctor.Street,
            }

        ).ToListAsync(cancellationToken);

        return response.AsQueryable();

    }
}
