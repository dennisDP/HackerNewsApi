using HackerNewsApi.Data;
using HackerNewsApi.Services.DTOs;

namespace HackerNewsApi.Services;

public class HackerNewsService : IHackerNewsService
{
    private readonly IHackerNewsApi _hackerNewsApi;

    public HackerNewsService(IHackerNewsApi hackerNewsApi)
    {
        _hackerNewsApi = hackerNewsApi;
    }

    public async Task<List<HackerNewsStory>> GetBestStories(int limit)
    {
        var result = new List<Data.Models.HackerNewsStory?>();
        var bestStoriesIds =
            (await _hackerNewsApi.GetBestStoryIdsAsync() ?? throw new InvalidOperationException()).Take(limit);

        foreach (var id in bestStoriesIds)
        {
            var story = await _hackerNewsApi.GetStoryAsync(id);
            result.Add(story);
        }

        return BuildDTO(result);
    }

    private List<HackerNewsStory> BuildDTO(List<Data.Models.HackerNewsStory?> stories)
    {
        return stories.Select(story => new HackerNewsStory
        {
            Title = story?.Title,
            Uri = story?.Url,
            PostedBy = story?.By,
            Time = story?.Time != null ? DateTimeOffset.FromUnixTimeSeconds(story.Time).DateTime : null,
            Score = story?.Score,
            CommentCount = story?.Kids?.Length ?? 0
        }).OrderByDescending(story => story.Score).ToList();
    }
}