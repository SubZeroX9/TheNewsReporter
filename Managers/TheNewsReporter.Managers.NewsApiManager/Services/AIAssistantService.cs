using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TheNewsReporter.Managers.NewsApiManager.Models.News;
using TheNewsReporter.Managers.NewsApiManager.Models.Requests;
using TheNewsReporter.Managers.NewsApiManager.Models.Responses;
using TheNewsReporter.Managers.NewsApiManager.Models.UserPreferences;

namespace TheNewsReporter.Managers.NewsApiManager.Services
{
    public class AIAssistantService
    {
        private readonly ILogger<AIAssistantService> _logger;
        private readonly DaprClient _daprClient;

        public AIAssistantService(ILogger<AIAssistantService> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        public async Task<AIAssistantRecAndSumResponse> GetArticlesRecomendations(UserPreference userPreferences, List<NewsArticle> news)
        {
            _logger.LogInformation("Getting articles from AI Assistant service");
            try
            {
                var userPreferencesJson = JsonSerializer.Serialize(userPreferences);
                var newsJson = JsonSerializer.Serialize(news);

                var aiAssistantRequest = new AIAssistantRequest
                {
                    UserPreferences = new Preferences
                    {
                        Categories = userPreferences.Categories,
                        NewsPreferences = userPreferences.NewsPreferences
                    },
                    Articles = news
                };

                var response = await _daprClient.InvokeMethodAsync<AIAssistantRequest, AIAssistantRecAndSumResponse>("ai-assistant-service", "/recommendandsummerizequeue", aiAssistantRequest);

                //await _daprClient.InvokeBindingAsync < AIAssistantRequest("recommendandsummerizequeue", "create", aiAssistantRequest);


                _logger.LogInformation("Articles retrieved successfully from AI Assistant service");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in getting articles from AI Assistant service, ex: {ex}", ex.Message);

                var innesrEx = ex.InnerException;

                if (innesrEx != null)
                {
                    _logger.LogError("Inner exception: {innesrEx}", innesrEx.Message);
                }

                throw;
            }
        }
    }
}
