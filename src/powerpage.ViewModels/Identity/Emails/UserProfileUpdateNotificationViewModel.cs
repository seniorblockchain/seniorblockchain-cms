using powerpage.Entities.Identity;

namespace powerpage.ViewModels.Identity.Emails;

public class UserProfileUpdateNotificationViewModel : EmailsBase
{
    public User User { set; get; }
}