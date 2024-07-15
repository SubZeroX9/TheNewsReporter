using Dapr.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TheNewsReporter.Managers.NewsApiManager.Models.Requests;
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
        private readonly NotificationService _notificationService;

        public NewsApiController(ILogger<NewsApiController> logger, AIAssistantService aiAssistantService, NewsAggregationService newsAggregationService, UserPreferenceService userPreferenceService, NotificationService notificationService)
        {
            _logger = logger;
            _aiAssistantService = aiAssistantService;
            _newsAggregationService = newsAggregationService;
            _userPreferenceService = userPreferenceService;
            _notificationService = notificationService;
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
                _logger.LogError("Error in getting user preferences, ex: {ex}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in getting user preferences");
            }
        }

        [HttpPost("setpreferences")]
        public async Task<ActionResult> SetUserPreferences(UserPreferenceAddRequest userPreference)
        {
            _logger.LogInformation("Setting user preferences in controller");
            try
            {
                await _userPreferenceService.SetUserPreferences(userPreference);
                _logger.LogInformation("User preferences set successfully");
                return Ok("User Preferences Set Succesfuly");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in setting user preferences, ex: {ex}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in setting user preferences");
            }
        }

        [HttpPut("updateprefes")]
        public async Task<ActionResult> UpdateUserPreferences(UserPreferenceUpdateRequest userPreference)
        {
            _logger.LogInformation("Updating user preferences in controller");
            try
            {
                await _userPreferenceService.UpdateUserPreferences(userPreference);
                _logger.LogInformation("User preferences updated successfully");
                return Ok("User Preferences Updated Succesfuly");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in updating user preferences, ex: {ex}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in updating user preferences");
            }
        }

        [HttpGet("getnews/{id}")]
        public async Task<ActionResult> GetNews(string id)
        {
            _logger.LogInformation("Getting news in controller");
            try
            {
                var userPreferences = await _userPreferenceService.GetUserPreferenceById(id);
                _logger.LogInformation("User preferences retrieved successfully");
                var news = await _newsAggregationService.GetNews(userPreferences);
                _logger.LogInformation("News retrieved successfully");

                _logger.LogInformation("#######################################");

                var articles = await _aiAssistantService.GetArticlesRecomendations(userPreferences, news);
                _logger.LogInformation("Returning articles from AI Assistant service");
                _logger.LogInformation("Articles: {articles}", JsonSerializer.Serialize(articles));

                await _notificationService.SendNotificationAsync(userPreferences.CommunicationChannel, articles);

                _logger.LogInformation("Sent Proccess Message in Controller Succesfully");
                return Accepted("Request Accepted News are being proccessed");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in getting news, ex: {ex}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in getting news");
            }
        }
    }
}
