using Dapr.Client;
using System.Text;
using System.Text.Json;
using TheNewsReporter.Managers.NewsApiManager.Models.Articles;
using TheNewsReporter.Managers.NewsApiManager.Models.Requests;
using TheNewsReporter.Managers.NewsApiManager.Models.Responses;
using TheNewsReporter.Managers.NewsApiManager.Models.UserPreferences;

namespace TheNewsReporter.Managers.NewsApiManager.Services
{
    public class NotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly DaprClient _daprClient;
        private readonly StringBuilder _notificationMessage;
        public NotificationService(ILogger<NotificationService> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
            _notificationMessage = new StringBuilder();
        }

        public async Task SendNotificationAsync(CommunicationChannel communicationChannel, AIAssistantRecAndSumResponse articles)
        {
            _logger.LogInformation("Sending notification to user");

            NotificationRequest notificationRequest = new NotificationRequest
            {
                Channel = communicationChannel,
                Message = articles!= null && articles.Result.Count > 0 ? BuildNotificationMessage(articles.Result) : BuildEmptyNotificationMessage()
            };

            try
            {
                var metadata = new Dictionary<string, string>(){
                { "cloudevent.datacontenttype", "application/*+json" }
                };

                await _daprClient.PublishEventAsync("notification-pub-sub", "nots", notificationRequest, metadata);

                _logger.LogInformation("Notification sent successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in sending notification, ex: {ex}", ex.Message);
                var innerException = ex.InnerException;
                if (innerException != null)
                {
                    _logger.LogError("Inner Exception: {innerException.Message}", innerException.Message);
                }
                throw;
            }
        }

        private string BuildNotificationMessage(List<RecAndSumArticle> articles)
        {
            _notificationMessage.Clear();

            _notificationMessage.AppendLine("Hello, Dear User!");
            _notificationMessage.AppendLine("We have some news for you!\n");
            int index = 1;
            foreach (var article in articles)
            {
                _notificationMessage.AppendLine($"{index}. {article.Title}");
                _notificationMessage.AppendLine($"{article.Description}");
                _notificationMessage.AppendLine("If you wish to read the full story Enter the link below.");
                _notificationMessage.AppendLine($"Link: {article.Link}");
                _notificationMessage.AppendLine();
                _notificationMessage.AppendLine("Otherwise Enjoy this quick summery.");
                _notificationMessage.AppendLine();
                _notificationMessage.AppendLine($"{article.Summary}");
                _notificationMessage.AppendLine("\n");
                index++;
            }

            _notificationMessage.AppendLine("Thank you for using our service!");
            _notificationMessage.AppendLine("Yours, The News Reporter Team");

            return _notificationMessage.ToString();
        }

        private string BuildEmptyNotificationMessage()
        {
            _notificationMessage.Clear();

            _notificationMessage.AppendLine("Hello, Dear User!");
            _notificationMessage.AppendLine("We have no news for you at the moment.");
            _notificationMessage.AppendLine("Check in with us later.");
            _notificationMessage.AppendLine("Thank you for using our service!");
            _notificationMessage.AppendLine("Yours, The News Reporter Team");

            return _notificationMessage.ToString();
        }
    }
}
