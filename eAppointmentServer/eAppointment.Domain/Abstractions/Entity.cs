using eAppointment.Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace eAppointment.Domain.Abstractions;

public abstract class Entity
{
    public Entity()
    {
        Id = Guid.CreateVersion7();
    }

    #region Audit Log

    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Guid CreatedUserId { get; set; } = default!;
    public DateTimeOffset? UpdatedAt { get; set; }
    public Guid? UpdatedUserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public Guid? DeletedUserId { get; set; }
    public bool IsActive { get; set; } = true;

    #endregion
}