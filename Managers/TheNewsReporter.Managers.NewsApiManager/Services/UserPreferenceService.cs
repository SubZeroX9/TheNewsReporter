
using Dapr.Client;
using TheNewsReporter.Managers.NewsApiManager.Models.UserPreferences;

namespace TheNewsReporter.Managers.NewsApiManager.Services
{
    public class UserPreferenceService
    {
        private readonly ILogger<UserPreferenceService> _logger;
        private readonly DaprClient _daprClient;

        public UserPreferenceService(ILogger<UserPreferenceService> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        public async Task<List<UserPreference>> GetAllUserPreferences()
        {
            _logger.LogInformation("Getting all user preferences in user pref service using dapr");
            try
            {
                var userPreferences = await _daprClient.InvokeMethodAsync<List<UserPreference>>(HttpMethod.Get, "user-preferences-service", "/api/UserPreferences/alluserpreferences");
                _logger.LogInformation("User preferences retrieved successfully");
                return userPreferences;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in getting user preferences In user prefs Service Message: {message}", ex.Message);
                throw;
            }
        }
    }
}
