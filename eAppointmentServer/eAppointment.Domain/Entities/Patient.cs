using eAppointment.Domain.Abstractions;
using eAppointment.Domain.CommonRecords;

namespace eAppointment.Domain.Entities;

public sealed class Patient : Entity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string IdentityNumber { get; set; } = default!;
    public string FullName => string.Join(" ", FirstName, LastName);
    public Address Address { get; set; } = default!;
    public PersonalInformation PersonalInformation { get; set; } = default!;
    
}