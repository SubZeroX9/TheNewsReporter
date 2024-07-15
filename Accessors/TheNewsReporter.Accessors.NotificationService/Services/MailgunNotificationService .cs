using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TheNewsReporter.Accessors.NotificationApiService.Models;

namespace TheNewsReporter.Accessors.NotificationApiService.Services
{
    public class MailgunNotificationService : INotificationService
    {
        private readonly ILogger<MailgunNotificationService> _logger;
        private readonly HttpClient _client;
        private readonly MailGunApiSettings _settings;

        public MailgunNotificationService(IOptions<MailGunApiSettings> options, ILogger<MailgunNotificationService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _client = httpClient;
            _settings = options.Value;
            SetHttpClientParams();
            //_logger.LogInformation("Api key: {apikey}",_settings.ApiKey);
        }

        private void SetHttpClientParams()
        {
            _client.BaseAddress = new Uri($"{_settings.BaseUrl}/{_settings.Domain}/messages");
            var byteArray = Encoding.ASCII.GetBytes($"api:{_settings.ApiKey}");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(@"Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<bool> SendNotificationAsync(NotificationRequest notificationRequest)
        {
            _logger.LogInformation("Sending notification using MailGun Service");
            try
            {
                var recipient = notificationRequest.Channel.Details["email"];

                MultipartFormDataContent content = new MultipartFormDataContent
                {
                    { new StringContent($"{_settings.DisplayName}  <news_reporter@{_settings.Domain}>"), "from" },
                    { new StringContent(recipient), "to" },
                    { new StringContent("Latest News"), "subject" },
                    { new StringContent(notificationRequest.Message), "text" }
                };


                var request = await _client.PostAsync(string.Empty, content);
                request.EnsureSuccessStatusCode();

                string response = await request.Content.ReadAsStringAsync();

                _logger.LogInformation($"Notification sent successfully. Response: {JsonSerializer.Serialize(response)}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send notification, ex: {ex}", ex.Message);

                var innestException = ex.InnerException;

                while (innestException != null)
                {
                    _logger.LogError("Inner exception: {innestException}", innestException.Message);
                    innestException = innestException.InnerException;
                }

                return false;
            }
        }
    }
}
