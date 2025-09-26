namespace eAppointment.Domain.CommonRecords;
public sealed record Address
{
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
}