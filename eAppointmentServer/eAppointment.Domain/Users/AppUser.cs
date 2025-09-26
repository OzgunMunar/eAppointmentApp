using Microsoft.AspNetCore.Identity;

namespace eAppointment.Domain.Users;

public sealed class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string FullName => $"{FirstName} {LastName}";

    #region Audit Log

    public DateTimeOffset CreatedAt { get; set; }
    public Guid CreatedUserId { get; set; } = default!;
    public DateTimeOffset? UpdatedAt { get; set; }
    public Guid? UpdatedUserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public Guid? DeletedUserId { get; set; }

    #endregion

}