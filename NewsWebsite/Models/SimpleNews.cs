
using NewsWebsite.Data;

namespace NewWebsite.Models;

public class SimpleNews
{
    public int NewsId { get; set; }
    public required string Title { get; set; }

    public required string Content { get; set; }
    public string? Summary { get; set; }

    public DateTime CreatedDate { get; set; }
    public required string CreatedDateString { get; set; }

    public int UserId { get; set; }
    public required User User { get; set; }


    public NewsStatus NewsStatus { get; set; }

    public DateTime? PublishedDate { get; set; }
    public bool IsPublished { get; set; }

    public string? ImageUrl { get; set; }

    public int NewsCategoryId { get; set; }
    public required string NewsCategory { get; set; }

}

