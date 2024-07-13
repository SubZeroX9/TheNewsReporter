
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using TheNewsReporter.Managers.NewsApiManager.Models.News;
using TheNewsReporter.Managers.NewsApiManager.Models.Requests;
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
                var userPreferences = await _daprClient.InvokeMethodAsync<List<UserPreference>>(HttpMethod.Get, "user-preferences-service", "/alluserpreferences");
                _logger.LogInformation("User preferences retrieved successfully");
                return userPreferences;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in getting user preferences In user prefs Service Message: {message}", ex.Message);
                throw;
            }
        }

        public async Task UpdateUserPreferences(UserPreferenceUpdateRequest userPreference)
        {
            _logger.LogInformation("Updating user preferences in user pref service using dapr");
            try
            {
                await _daprClient.InvokeMethodAsync(HttpMethod.Put, "user-preferences-service", "/updateuserpreferencesqueue", userPreference);
                //await _daprClient.InvokeBindingAsync("updateuserpreferencesqueue", "create", userPreference);

                _logger.LogInformation("User preferences updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in updating user preferences In user prefs Service Message: {message}", ex.Message);
                throw;
            }
        }

        public async Task SetUserPreferences(UserPreferenceAddRequest userPreference)
        {
            _logger.LogInformation("Setting user preferences in user pref service using dapr");
            try
            {
                await _daprClient.InvokeBindingAsync("setuserpreferencesqueue", "create", userPreference);

                _logger.LogInformation("User preferences set successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in setting user preferences In user prefs Service Message: {message}", ex.Message);
                throw;
            }
        }

        public async Task<UserPreference> GetUserPreferenceById(string id)
        {
            _logger.LogInformation("Getting user preference by id in user pref service using dapr");
            try
            {
                var userPreference = await _daprClient.InvokeMethodAsync<UserPreference>(HttpMethod.Get, "user-preferences-service", $"/{id}");
                _logger.LogInformation("User preference retrieved successfully");
                return userPreference;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in getting user preference by id In user prefs Service Message: {message}", ex.Message);
                throw;
            }
        }
    }
}
