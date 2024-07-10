namespace TheNewsReporter.Accessors.UserPreferencesService.Models
{
    public record UserPreferenceUpdateRequest
    {
        public string? UserId { get; set; }
        public List<string>? Categories { get; set; }
        public List<string>? NewsPreferences { get; set; }
        public CommunicationChannel? CommunicationChannel { get; set; }
    }
}
