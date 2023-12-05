using powerpage.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace powerpage.DataLayer.Configurations;

public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.HasOne(userLogin => userLogin.User)
            .WithMany(user => user.Logins)
            .HasForeignKey(userLogin => userLogin.UserId);

        builder.ToTable("AppUserLogins");
    }
}