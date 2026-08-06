public class SoftwareCardViewModel
    {
    public List<SoftwareCard> Cards { get; set; } = new();
    }

public class SoftwareCard
    {
    public string Title { get; set; }

    public string Description { get; set; }
    public string Icon { get; set; }
    public List<DownloadItem> Downloads { get; set; } = new();
    }

public class DownloadItem
    {
    public string Title { get; set; }

    public string Size { get; set; }

    public string Url { get; set; }

    }