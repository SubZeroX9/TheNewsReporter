using System.Text.Json.Serialization;

namespace TheNewsReporter.Accessors.NotificationApiService.Models
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
        [JsonPropertyName("channel")]
        public string ComChannel
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

        [JsonIgnore]
        public Channel ChannelEnum { get; set; } = Channel.NULL;

        [JsonPropertyName("details")]
        public Dictionary<string, string> Details { get; set; }
    }
}
