using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheNewsReporter.Accessors.NotificationApiService.Models;
using TheNewsReporter.Accessors.NotificationApiService.Services;

namespace TheNewsReporter.Accessors.NotificationApiService.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost("/send-notification")]
        public async Task<IActionResult> SendNotificationAsync([FromBody] NotificationRequest notificationRequest)
        {
            _logger.LogInformation("Sending notification In Controller");
            try
            {
                bool result = await _notificationService.SendNotificationAsync(notificationRequest);

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
