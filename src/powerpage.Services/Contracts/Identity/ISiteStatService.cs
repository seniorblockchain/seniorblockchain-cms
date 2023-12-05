using powerpage.Entities.Identity;
using System.Security.Claims;
using powerpage.ViewModels.Identity;

namespace powerpage.Services.Contracts.Identity;

public interface ISiteStatService
{
    Task<List<User>> GetOnlineUsersListAsync(int numbersToTake, int minutesToTake);

    Task<List<User>> GetTodayBirthdayListAsync();

    Task UpdateUserLastVisitDateTimeAsync(ClaimsPrincipal claimsPrincipal);

    Task<AgeStatViewModel> GetUsersAverageAge();
}