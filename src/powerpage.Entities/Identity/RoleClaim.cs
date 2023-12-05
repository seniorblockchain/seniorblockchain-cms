using powerpage.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace powerpage.Entities.Identity;

public class RoleClaim : IdentityRoleClaim<int>, IAuditableEntity
{
    public virtual Role Role { get; set; }
}