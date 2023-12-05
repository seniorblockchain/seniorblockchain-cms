using powerpage.Entities.Identity;

namespace powerpage.ViewModels.Identity;

public class TodayBirthDaysViewModel
{
    public List<User> Users { set; get; }

    public AgeStatViewModel AgeStat { set; get; }
}