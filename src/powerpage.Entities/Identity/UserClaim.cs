using powerpage.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace powerpage.Entities.Identity;

public class UserClaim : IdentityUserClaim<int>, IAuditableEntity
{
    public virtual User User { get; set; }
}