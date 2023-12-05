using powerpage.Entities.Identity;
using cloudscribe.Web.Pagination;

namespace powerpage.ViewModels.Identity;

public class PagedAppLogItemsViewModel
{
    public PagedAppLogItemsViewModel()
    {
        Paging = new PaginationSettings();
    }

    public string LogLevel { get; set; } = string.Empty;

    public List<AppLogItem> AppLogItems { get; set; }

    public PaginationSettings Paging { get; set; }
}