using eAppointment.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAppointment.Infrastructure.Configurations;

public sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {

        builder.Property(field => field.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

    }
}