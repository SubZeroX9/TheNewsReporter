using Dapr;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using TheNewsReporter.Accessors.NotificationApiService.Models;
using TheNewsReporter.Accessors.NotificationApiService.Services;

namespace TheNewsReporter.Accessors.NotificationApiService.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;
        private readonly NotificationService _notificationService;

        public NotificationController(ILogger<NotificationController> logger, NotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        [Topic("notification-pub-sub", "notifications")]
        [HttpPost("/sendnotification")]
        public async Task<IActionResult> SendNotificationAsync([FromBody] CloudEvent<NotificationRequest> notificationRequest)
        {
            _logger.LogInformation("Sending notification In Controller");

            try
            {

                _logger.LogInformation("notificationRequest recived: {notificationRequest}",JsonSerializer.Serialize(notificationRequest.Data));

                bool result = await _notificationService.SendNotificationAsync(notificationRequest.Data);

                if (!result)
                {
                    _logger.LogError($"Failed to send notification. In Notification Controller");
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                _logger.LogInformation($"Notification sent to channel. In Notification Controller");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send notification. In Notification Controller. Ex: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
