using HackerNewsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApi.Controllers;

[ApiController]
[Route("/api")]
[AllowAnonymous]
[Produces("application/json")]
public class BestStoriesController : ControllerBase
{
    private readonly IHackerNewsService _hackerNewsService;
    private readonly ILogger<BestStoriesController> _logger;

    public BestStoriesController(ILogger<BestStoriesController> logger, IHackerNewsService hackerNewsService)
    {
        _logger = logger;
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet("best-stories/{limit}", Name = "BestStories_Get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int limit)
    {
        _logger.LogInformation("Getting {0} best HackerNews stories", limit);
        var result = await _hackerNewsService.GetBestStories(limit);
        return Ok(result);
    }
}