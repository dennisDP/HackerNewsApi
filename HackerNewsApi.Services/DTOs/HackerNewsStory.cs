namespace HackerNewsApi.Services.DTOs;

public class HackerNewsStory
{
    public string? Title { get; set; }
    public string? Uri { get; set; }
    public string? PostedBy { get; set; }
    public DateTime? Time { get; set; }
    public int? Score { get; set; }
    public int CommentCount { get; set; }
}