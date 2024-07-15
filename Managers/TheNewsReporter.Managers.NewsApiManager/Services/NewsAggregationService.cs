using Dapr.Client;
using TheNewsReporter.Managers.NewsApiManager.Models.News;
using TheNewsReporter.Managers.NewsApiManager.Models.UserPreferences;

namespace TheNewsReporter.Managers.NewsApiManager.Services
{
    public class NewsAggregationService
    {
        private readonly ILogger<NewsAggregationService> _logger;
        private readonly DaprClient _daprClient;

        public NewsAggregationService(ILogger<NewsAggregationService> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        public async Task<List<NewsArticle>> GetNews(UserPreference userPreference)
        {
            _logger.LogInformation("Getting news in news aggregation service using dapr");
            try
            {
                var url = $"/api/NewsAggregation/latest-news-by-category?";
                foreach (var category in userPreference.Categories)
                {
                    url += $"categories={category}&";
                }

                var news = await _daprClient.InvokeMethodAsync<List<NewsArticle>>(HttpMethod.Get, "news-aggregation-service", url);
                _logger.LogInformation("News retrieved successfully");
                return news;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in getting news In news aggregation service Message: {message}", ex.Message);
                throw;
            }
        }
    }
}
