using powerpage.Entities.Identity;

namespace powerpage.ViewModels.Identity.Emails;

public class RegisterEmailConfirmationViewModel : EmailsBase
{
    public User User { set; get; }
    public string EmailConfirmationToken { set; get; }
}