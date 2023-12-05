using powerpage.Services.Contracts.Identity;
using powerpage.ViewModels.Identity;
using Microsoft.AspNetCore.Mvc;

namespace powerpage.Areas.Dashboard.ViewComponents;

public class OnlineUsersViewComponent : ViewComponent
{
    private readonly ISiteStatService _siteStatService;

    public OnlineUsersViewComponent(ISiteStatService siteStatService)
    {
        _siteStatService = siteStatService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int numbersToTake, int minutesToTake, bool showMoreItemsLink)
    {
        var usersList = await _siteStatService.GetOnlineUsersListAsync(numbersToTake, minutesToTake);
        return View("~/Areas/Dashboard/Views/Shared/Components/OnlineUsers/Default.cshtml",
            new OnlineUsersViewModel
            {
                MinutesToTake = minutesToTake,
                NumbersToTake = numbersToTake,
                ShowMoreItemsLink = showMoreItemsLink,
                Users = usersList
            });
    }
}