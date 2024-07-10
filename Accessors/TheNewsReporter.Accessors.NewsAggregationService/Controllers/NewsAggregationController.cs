using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheNewsReporter.Accessors.NewsAggregationService.Models;
using TheNewsReporter.Accessors.NewsAggregationService.Services;
using TheNewsReporter.Accessors.NewsAggregationService.Utils;

namespace TheNewsReporter.Accessors.NewsAggregationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsAggregationController : ControllerBase
    {
        private readonly ILogger<NewsAggregationController> _logger;
        private readonly NewsService _newsService;

        public NewsAggregationController(ILogger<NewsAggregationController> logger, NewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        [HttpGet("latest-news")]
        public async Task<ActionResult<List<NewsArticle>>> GetLatestNews(int size = 10,string language = "en")
        {
            try
            {
                var latesNews = await _newsService.GetLatestNewsAsync(size, language);
                return Ok(latesNews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the latest news.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("latest-news-by-category")]
        public async Task<ActionResult<List<NewsArticle>>> GetLatestNewsByCategory([FromQuery] List<string> categories, [FromQuery] int sizepercategory=10, string language = "en")
        {
            _logger.LogInformation("Entering GetLatestNewsByCategory inside NewsAggregationController");
            try
            {
                var latesNews = await _newsService.GetLatestNewsByCategoryAsync(categories, sizepercategory, language);
                _logger.LogInformation("Returning latest news by category in NewsAggregationController");
                return Ok(latesNews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the latest news by category.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
