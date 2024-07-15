using System.Text.Json.Serialization;

namespace TheNewsReporter.Accessors.AIAssistentService.Models
{
    public class Preferences
    {
        [JsonPropertyName("categories")]
        public List<string> Categories { get; set; }
        
        [JsonPropertyName("news_preferences")]
        public List<string> NewsPreferences { get; set; }
    }
}
