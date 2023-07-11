using HackerNewsApi.Data.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace HackerNewsApi.Data;

public class HackerNewsApiCache : IHackerNewsApi
{
    private readonly HackerNewsApiClient _apiClient;
    private readonly IMemoryCache _cache;
    private readonly CacheOptions _cacheOptions;

    public HackerNewsApiCache(HackerNewsApiClient apiClient, IMemoryCache cache, IOptions<CacheOptions> cacheOptions)
    {
        _apiClient = apiClient;
        _cache = cache;
        _cacheOptions = cacheOptions.Value;
    }

    public async Task<List<int>> GetBestStoryIdsAsync()
    {
        const string cacheKey = "BestStoryIds";
        if (_cache.TryGetValue(cacheKey, out List<int> storyIds))
        {
            return storyIds;
        }

        storyIds = await _apiClient.GetBestStoryIdsAsync();
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(_cacheOptions.ExpirationTimeMinutes));
        _cache.Set(cacheKey, storyIds, cacheOptions);

        return storyIds;
    }

    public async Task<HackerNewsStory> GetStoryAsync(int storyId)
    {
        var cacheKey = $"Story_{storyId}";
        if (_cache.TryGetValue(cacheKey, out HackerNewsStory? story))
        {
            return story;
        }

        story = await _apiClient.GetStoryAsync(storyId);
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(_cacheOptions.ExpirationTimeMinutes));
        _cache.Set(cacheKey, story, cacheOptions);

        return story;
    }
}