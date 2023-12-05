namespace powerpage.Common.WebToolkit;

public static class ServerInfo
{
    public static string GetAppDataFolderPath()
    {
        var appDataFolderPath = Path.Combine(GetWwwRootPath(), "App_Data");
        if (!Directory.Exists(appDataFolderPath))
        {
            Directory.CreateDirectory(appDataFolderPath);
        }

        return appDataFolderPath;
    }

    private static readonly string[] separator = new[] { "bin" };

    public static string GetWwwRootPath()
    {
        return Path.Combine(
                            AppContext.BaseDirectory.Split(separator, StringSplitOptions.RemoveEmptyEntries)[0],
                            "wwwroot");
    }
}