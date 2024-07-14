namespace TheNewsReporter.Accessors.NotificationApiService.Models
{
    public record NotificationRequest
    {
        public required CommunicationChannel Channel { get; init; }
        public required string Message { get; init; }
    }
}
