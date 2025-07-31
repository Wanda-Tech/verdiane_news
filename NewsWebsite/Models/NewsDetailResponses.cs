
namespace NewWebsite.Models;

public class NewsDetailResponse
{
    public required SimpleNews News { get; set; }

    public required List<SimpleNews> ReadNextNews { get; set; }

}

