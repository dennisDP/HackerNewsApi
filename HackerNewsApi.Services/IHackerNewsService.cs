using HackerNewsApi.Services.DTOs;

namespace HackerNewsApi.Services;

public interface IHackerNewsService
{
    Task<List<HackerNewsStory>> GetBestStories(int limit);
}