using System.Text.Json.Serialization;
using TheNewsReporter.Accessors.AIAssistentService.Models.Articles;

namespace TheNewsReporter.Accessors.AIAssistentService.Models.Responses
{
    public class AIAssistantRecommededResponse
    {
        public List<RecommendedArticle> Result { get; set; }
    }
}
