using System.Reflection;
using eAppointment.Domain.Abstractions;
using eAppointment.Domain.Employees;
using eAppointment.Domain.Entities;
using eAppointment.Domain.Users;
using eAppointment.Infrastructure.Configurations;
using GenericRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eAppointment.Infrastructure.Context;

// public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUnitOfWork
public class ApplicationDbContext : IdentityDbContext<
    AppUser,
    AppRole,
    Guid,
    IdentityUserClaim<Guid>,
    AppUserRole,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>
    >, IUnitOfWork
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<Patient> Patients { get; set; } = default!;
    public DbSet<Doctor> Doctors { get; set; } = default!;
    public DbSet<Appointment> Appointments { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Ignore<IdentityUserLogin<Guid>>();
        modelBuilder.Ignore<IdentityUserPasskey<Guid>>();
        modelBuilder.Ignore<IdentityUserRole<Guid>>();
        modelBuilder.Ignore<IdentityUserToken<Guid>>();
        modelBuilder.Ignore<IdentityUserClaim<Guid>>();
        modelBuilder.Ignore<IdentityRoleClaim<Guid>>();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
    
     public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        var entries = ChangeTracker.Entries<Entity>();

        // HttpContextAccessor httpContextAccessor = new();
        // string userIdString = httpContextAccessor.HttpContext!.User.Claims.First(p => p.Type == "user-id").Value;
        string? userIdString = _httpContextAccessor.HttpContext?.User?.Claims
            .FirstOrDefault(c => c.Type == "user-id")?.Value;

        // Guid userId = Guid.Parse(userIdString);
        Guid? userId = userIdString is null ? null : Guid.Parse(userIdString);


        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(p => p.CreatedAt)
                    .CurrentValue = DateTimeOffset.Now;
                if (userId.HasValue)
                {
                    entry.Property(p => p.CreatedUserId).CurrentValue = userId.Value;
                }
                    
            }

            if (entry.State == EntityState.Modified)
            {
                if (entry.Property(p => p.IsDeleted).CurrentValue == true)
                {
                    entry.Property(p => p.DeletedAt)
                        .CurrentValue = DateTimeOffset.Now;
                    if (userId.HasValue)
                    {
                        entry.Property(p => p.UpdatedUserId).CurrentValue = userId.Value;
                    }
                }
                else
                {
                    entry.Property(p => p.UpdatedAt)
                        .CurrentValue = DateTimeOffset.Now;
                    if (userId.HasValue)
                    {
                        entry.Property(p => p.DeletedUserId).CurrentValue = userId.Value;
                    }
                }
            }

            if (entry.State == EntityState.Deleted)
            {
                throw new ArgumentException("You can not delete any record!");
            }

        }
        return base.SaveChangesAsync(cancellationToken);
    }

}