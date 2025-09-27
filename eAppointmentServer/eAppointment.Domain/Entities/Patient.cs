using eAppointment.Domain.Abstractions;

namespace eAppointment.Domain.Entities;

public sealed class Patient : Entity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string IdentityNumber { get; set; } = default!;
    public string FullName => string.Join(" ", FirstName, LastName);
    public string Country { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    
}