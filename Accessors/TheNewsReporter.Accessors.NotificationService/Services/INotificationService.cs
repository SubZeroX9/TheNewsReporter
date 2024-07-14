using TheNewsReporter.Accessors.NotificationApiService.Models;

namespace TheNewsReporter.Accessors.NotificationApiService.Services
{
    public interface INotificationService
    {
        Task<bool> SendNotificationAsync(NotificationRequest notificationRequest);
    }
}
