using System.Text.Json.Serialization;
using TheNewsReporter.Managers.NewsApiManager.Models.News;
using TheNewsReporter.Managers.NewsApiManager.Models.UserPreferences;

namespace TheNewsReporter.Managers.NewsApiManager.Models.Requests
{
    public class AIAssistantRequest
    {
        [JsonPropertyName("user_preferences")]
        public Preferences UserPreferences { get; set; }

        [JsonPropertyName("articles")]
        public List<NewsArticle> Articles { get; set; }
    }
}
