using eAppointment.Domain.Abstractions;
using eAppointment.Domain.CommonRecords;
using eAppointmentServer.Domain.Enums;

namespace eAppointment.Domain.Entities;

public sealed class Doctor : Entity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string IdentityNumber { get; set; } = default!;
    public string FullName => string.Join(" ", FirstName, LastName);
    public Address Address { get; set; } = default!;
    public PersonalInformation PersonalInformation { get; set; } = default!;
    public DepartmentEnum Department { get; set; } = DepartmentEnum.Acil;
    
}