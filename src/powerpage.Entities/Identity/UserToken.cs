using powerpage.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace powerpage.Entities.Identity;

public class UserToken : IdentityUserToken<int>, IAuditableEntity
{
    public virtual User User { get; set; }
}