using powerpage.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace powerpage.Entities.Identity;

public class UserRole : IdentityUserRole<int>, IAuditableEntity
{
    public virtual User User { get; set; }

    public virtual Role Role { get; set; }
}