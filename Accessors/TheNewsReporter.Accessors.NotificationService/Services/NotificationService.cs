using TheNewsReporter.Accessors.NotificationApiService.Models;

namespace TheNewsReporter.Accessors.NotificationApiService.Services
{
    public class NotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly Dictionary<Channel, INotificationService> _services;

        public NotificationService(ILogger<NotificationService> logger, MailgunNotificationService mailgunNotificationService)
        {
            _logger = logger;
            _services = new Dictionary<Channel, INotificationService>
            {
                { Channel.Email, mailgunNotificationService }
            };
        }

        public async Task<bool> SendNotificationAsync(NotificationRequest notificationRequest)
        {
            Channel channel = notificationRequest.Channel.ChannelEnum;
            _logger.LogInformation($"Sending notification to {channel} channel. In Notification Service");

            if (!_services.ContainsKey(channel))
            {
                _logger.LogError($"Channel {channel} is not supported.");
                return false;
            }

            if (!IsValidDetails(notificationRequest.Channel))
            {
                _logger.LogError($"Invalid details for {channel} channel.");
                return false;
            }

            bool result = await _services[channel].SendNotificationAsync(notificationRequest);

            _logger.LogInformation($"Notification sent to {channel} channel. In Notification Service");
            return result;
        }

        private bool IsValidDetails(CommunicationChannel communicationChannel) {
            return communicationChannel.ChannelEnum switch
            {
                Channel.Email => communicationChannel.Details.ContainsKey("email"),
                Channel.Sms => communicationChannel.Details.ContainsKey("phoneNumber"),
                Channel.PushNotification => communicationChannel.Details.ContainsKey("peviceId"),
                Channel.InAppNotification => communicationChannel.Details.ContainsKey("userId"),
                Channel.Telegram => communicationChannel.Details.ContainsKey("telegramId"),
                Channel.Slack => communicationChannel.Details.ContainsKey("slackId"),
                Channel.Discord => communicationChannel.Details.ContainsKey("discordId"),
                Channel.WhatsApp => communicationChannel.Details.ContainsKey("whatsAppId"),
                _ => false,
            };
        }


    }
}
