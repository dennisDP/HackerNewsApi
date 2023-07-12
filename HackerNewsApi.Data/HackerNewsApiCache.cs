using HackerNewsApi.Data.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HackerNewsApi.Data;

public class HackerNewsApiCache : IHackerNewsApiCache
{
    private readonly ILogger<HackerNewsApiCache> _logger;
    private readonly IHackerNewsApiClient _apiClient;
    private readonly IMemoryCache _cache;
    private readonly CacheOptions _cacheOptions;

    public HackerNewsApiCache(IHackerNewsApiClient apiClient, IMemoryCache cache, IOptions<CacheOptions> cacheOptions, ILogger<HackerNewsApiCache> logger)
    {
        _apiClient = apiClient;
        _cache = cache;
        _logger = logger;
        _cacheOptions = cacheOptions.Value;
    }

    public async Task<List<int>?> GetBestStoryIdsAsync()
    {
        const string cacheKey = "BestStoryIds";
        if (_cache.TryGetValue(cacheKey, out List<int>? storyIds))
        {
            _logger.LogInformation("Cache hit. Fetching best story ids from cache.");
            return storyIds;
        }

        _logger.LogInformation("Cache miss. Fetching best story ids from API.");
        storyIds = await _apiClient.GetBestStoryIdsAsync();
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(_cacheOptions.StoryIdsExpirationTimeMinutes));
        _cache.Set(cacheKey, storyIds, cacheOptions);

        return storyIds;
    }

    public async Task<HackerNewsStory?> GetStoryAsync(int storyId)
    {
        var cacheKey = $"Story_{storyId}";
        if (_cache.TryGetValue(cacheKey, out HackerNewsStory? story))
        {
            _logger.LogInformation("Cache hit. Fetching story {0} from cache.", storyId);
            return story;
        }

        _logger.LogInformation("Cache miss. Fetching story {0} from API.", storyId);
        story = await _apiClient.GetStoryAsync(storyId);
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(_cacheOptions.StoryExpirationTimeMinutes));
        _cache.Set(cacheKey, story, cacheOptions);

        return story;
    }
}