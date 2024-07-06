using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace TheNewsReporter.Accessors.UserPreferencesService.Models
{
    public class UserPreferences
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("user_id")]
        [JsonPropertyName("user_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        [BsonElement("prefered_categories")]
        [JsonPropertyName("prefered_categories")]
        public List<string>? Categories { get; set; }

        [BsonElement("communication_channel")]
        [JsonPropertyName("communication_channel")]
        public CommunicationChannel? CommunicationChannel { get; set; }
    }
}
