using HackerNewsApi.Services.DTOs;

namespace HackerNewsApi.Services;

public interface IHackerNewsService
{
    Task<HackerNewsStory[]> GetBestStories(int limit); 
}