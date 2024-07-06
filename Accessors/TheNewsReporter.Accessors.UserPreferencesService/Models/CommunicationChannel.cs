using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace TheNewsReporter.Accessors.UserPreferencesService.Models
{
    public enum Channel
    {
        NULL,
        Email,
        Sms,
        PushNotification,
        InAppNotification,
        Telegram,
        Slack,
        Discord,
        WhatsApp
    }

    public class CommunicationChannel
    {
        [BsonElement("channel")]
        [JsonPropertyName("channel")]
        public string? ComChannel
        {
            get => Enum.GetName(typeof(Channel), ChannelEnum);
            set
            {
                if (value != null && Enum.TryParse(value, true, out Channel result))
                {
                    ChannelEnum = result;
                }
                else
                {
                    ChannelEnum = Channel.NULL;
                }
            }
        }

        [BsonIgnore]
        [JsonIgnore]
        public Channel ChannelEnum { get; set; } = Channel.NULL;

        [BsonElement("details")]
        public Dictionary<string, string>? Details { get; set; }
    }
}
