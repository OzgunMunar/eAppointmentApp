namespace eAppointment.Domain.Employees;
public sealed record PersonalInformation
{
    public string TcNo { get; set; } = default!;
    public string? Email { get; set; }
    public string? PhoneNumber1 { get; set; }
    public string? PhoneNumber2 { get; set; }
}