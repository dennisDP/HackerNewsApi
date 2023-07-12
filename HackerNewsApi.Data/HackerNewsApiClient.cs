using System.Text.Json;
using HackerNewsApi.Data.Models;

namespace HackerNewsApi.Data;

public class HackerNewsApiClient : IHackerNewsApiClient
{
    private const string BaseUrl = "https://hacker-news.firebaseio.com/v0";
    private readonly HttpClient _httpClient;

    public HackerNewsApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<int>?> GetBestStoryIdsAsync()
    {
        var url = $"{BaseUrl}/beststories.json";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<int>>(content);
    }

    public async Task<HackerNewsStory?> GetStoryAsync(int storyId)
    {
        var url = $"{BaseUrl}/item/{storyId}.json";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<HackerNewsStory>(content);
    }
}