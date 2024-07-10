using Microsoft.Extensions.Options;
using TheNewsReporter.Accessors.NewsAggregationService.Models;
using TheNewsReporter.Accessors.NewsAggregationService.Utils;

namespace TheNewsReporter.Accessors.NewsAggregationService.Services
{
    public class NewsService
    {
        private readonly string _newsApiBaseUrl;

        private readonly HttpClient _httpClient;
        private readonly ILogger<NewsService> _logger;

        private readonly NewsRequestBuilder _newsRequestBuilder;

        private readonly int _pageSize;

        public NewsService(IOptions<NewsApiSettings> newsApiSettings, HttpClient httpClient, ILogger<NewsService> logger)
        {
            _pageSize = newsApiSettings.Value.PageSize;

            _newsRequestBuilder = new NewsRequestBuilder(newsApiSettings.Value.BaseUrl, newsApiSettings.Value.ApiKey);

            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<NewsArticle>> GetLatestNewsAsync(int size = 10, string language = "en")
        {
            _logger.LogInformation("Entering GetLatestNewsAsync inside NewsService");
            try
            {
                var articles = await GetLatestWithQueryParams(null, size,language);
                _logger.LogInformation("Returning latest news in NewsService");
                return articles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the latest news.");
                throw;
            }
        }

        public async Task<List<NewsArticle>> GetLatestNewsByCategoryAsync(List<string> categories,int size = 10, string language = "en")
        {
            _logger.LogInformation("Entering GetLatestNewsByCategoryAsync inside NewsService");
            try
            {
                var articles = await GetLatestWithQueryParams(categories, size,language);
                _logger.LogInformation("Returning latest news by category in NewsService");
                return articles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the latest news by category.");
                throw;
            }
        }

        private async Task<List<NewsArticle>> GetLatestWithQueryParams(List<string> categories, int size = 10,string language = "en")
        {
            categories = NewsCategoryValidator.ValidateCategories(categories);
            language = LanguageValidator.ValidateLanguage(language);

            try
            {
                var articles = new List<NewsArticle>();
                int sizeLeft;
                string nexPage;
                string url;
                string category;
                int index = 0;
                do
                {
                    sizeLeft = size;
                    nexPage = string.Empty;
                    category = string.Empty;
                    url = string.Empty;
                    while (sizeLeft > 0)
                    {
                        var pageSize = Math.Min(sizeLeft, _pageSize);
                        category = categories != null && categories.Count > 0 ? categories[index] : string.Empty;
                        url = _newsRequestBuilder
                            .Reset()
                            .SetEndpointLatest()
                            .SetLanguage(language)
                            .SetCategory(category)
                            .SetSize(pageSize)
                            .SetPage(nexPage)
                            .BuildUrl();

                        var response = await _httpClient.GetFromJsonAsync<NewsApiResponse>(url);
                        if (response == null || response.Results == null || response.Results.Count == 0)
                        {
                            break;
                        }
                        _logger.LogInformation($"Fetched {response.Results.Count} articles from {url}");
                        _logger.LogInformation($"Total Results: {response.TotalResults}");
                        articles.AddRange(response.Results);

                        if (response.NextPage == null)
                        {
                            break;

                        }
                        nexPage = response.NextPage;
                        sizeLeft -= _pageSize;
                    }
                    index++;
                } while (categories != null && index < categories.Count);
                
                return articles;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
