namespace HackerNewsApi.Data;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

public class HackerNewsApiClient : IHackerNewsApi
{
    private readonly HttpClient _httpClient;
    const string BaseUrl = "https://hacker-news.firebaseio.com/v0";

    public HackerNewsApiClient()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<int>> GetBestStoryIdsAsync()
    {
        var url = $"{BaseUrl}/beststories.json";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var storyIds = JsonSerializer.Deserialize<List<int>>(content);
        return storyIds;
    }

    public async Task<HackerNewsStory> GetStoryAsync(int storyId)
    {
        var url = $"{BaseUrl}/item/{storyId}.json";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var story = JsonSerializer.Deserialize<HackerNewsStory>(content);
        return story;
    }
}
