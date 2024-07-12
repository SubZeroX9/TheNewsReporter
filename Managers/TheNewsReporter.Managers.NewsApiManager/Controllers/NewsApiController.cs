using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheNewsReporter.Managers.NewsApiManager.Models.UserPreferences;
using TheNewsReporter.Managers.NewsApiManager.Services;

namespace TheNewsReporter.Managers.NewsApiManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsApiController : ControllerBase
    {
        private readonly ILogger<NewsApiController> _logger;
        private readonly AIAssistantService _aiAssistantService;
        private readonly NewsAggregationService _newsAggregationService;
        private readonly UserPreferenceService _userPreferenceService;

        public NewsApiController(ILogger<NewsApiController> logger, AIAssistantService aiAssistantService, NewsAggregationService newsAggregationService, UserPreferenceService userPreferenceService)
        {
            _logger = logger;
            _aiAssistantService = aiAssistantService;
            _newsAggregationService = newsAggregationService;
            _userPreferenceService = userPreferenceService;
        }

        [HttpGet("allprefes")]
        public async Task<ActionResult<List<UserPreference>>> GetAllUserPreferences()
        {
            _logger.LogInformation("Getting all user preferences in controller");
            try
            {
                var userPreferences = await _userPreferenceService.GetAllUserPreferences();
                _logger.LogInformation("User preferences retrieved successfully");
                return Ok(userPreferences);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in getting user preferences", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in getting user preferences");
            }
        }
    }
}
