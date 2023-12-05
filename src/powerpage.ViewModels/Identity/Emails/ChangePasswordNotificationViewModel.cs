using powerpage.Entities.Identity;

namespace powerpage.ViewModels.Identity.Emails;

public class ChangePasswordNotificationViewModel : EmailsBase
{
    public User User { set; get; }
}