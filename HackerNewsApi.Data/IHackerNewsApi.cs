using HackerNewsApi.Data.Models;

namespace HackerNewsApi.Data;

public interface IHackerNewsApi
{
    Task<List<int>> GetBestStoryIdsAsync();
    Task<HackerNewsStory> GetStoryAsync(int storyId);
}