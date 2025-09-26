namespace eAppointment.Domain.CommonRecords;
public sealed record PersonalInformation
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

}