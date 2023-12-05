using powerpage.Entities.AuditableEntity;
using powerpage.Entities.Identity;

namespace powerpage.Services.Identity.Logger;

public class LoggerItem
{
    public AppShadowProperties Props { set; get; }
    public AppLogItem AppLogItem { set; get; }
}