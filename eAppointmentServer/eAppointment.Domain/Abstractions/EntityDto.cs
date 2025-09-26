namespace eAppointment.Domain.Abstractions;

public abstract class EntityDto
{

    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public bool IsActive { get; set; }
    public Guid CreatedUserID { get; set; }
    public Guid? UpdatedUserID { get; set; }
    public string CreatedUserName { get; set; } = default!;
    public string? UpdatedUserName { get; set; }
}