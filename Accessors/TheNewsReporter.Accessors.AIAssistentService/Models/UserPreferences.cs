using System.Text.Json.Serialization;

namespace TheNewsReporter.Accessors.AIAssistentService.Models
{
    public class UserPreferences
    {
        public List<string> Categories { get; set; }
        
        [JsonPropertyName("news_preferences")]
        public List<string> NewsPreferences { get; set; }
    }
}
