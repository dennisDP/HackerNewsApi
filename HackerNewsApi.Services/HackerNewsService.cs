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

    public async Task<HackerNewsStory[]> GetBestStories(int limit)
    {
        throw new NotImplementedException();
    }
}