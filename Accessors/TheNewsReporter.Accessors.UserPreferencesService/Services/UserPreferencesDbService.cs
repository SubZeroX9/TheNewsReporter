using MongoDB.Bson;
using MongoDB.Driver;
using TheNewsReporter.Accessors.UserPreferencesService.Models;

namespace TheNewsReporter.Accessors.UserPreferencesService.Services
{
    public class UserPreferencesDbService
    {
        private readonly DbContext _db;
        private readonly ILogger<UserPreferencesDbService> _logger;

        public UserPreferencesDbService(DbContext db, ILogger<UserPreferencesDbService> logger)
        {
            _db = db;
            _logger = logger;
        }

        internal async Task<List<UserPreferences>> GetAllUserPreferencesAsync()
        {
            try
            {
                _logger.LogInformation("Entering GetAllUserPreferencesAsync inside UserPreferencesDbService");
                var userPreferences = await _db.UserPreferencesCollection.Find(new BsonDocument()).ToListAsync();
                _logger.LogInformation("Returning all user preferences in UserPreferencesDbService");
                return userPreferences;
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("Timeout error while getting all user preferences: {ex}", ex);
                throw;
            }
            catch (MongoException ex)
            {
                _logger.LogError("MongoDB error while getting all user preferences: {ex}", ex);
                throw; 
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error while getting all user preferences: {ex}", ex);
                throw;
            }
        }

        internal async Task<UserPreferences> GetUserPreferences(string userId)
        {
            try
            { 
                _logger.LogInformation("Entering GetUserPreferences inside UserPreferencesDbService");
                var userPreferences = await _db.UserPreferencesCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
                _logger.LogInformation("Returning user preferences in UserPreferencesDbService");
                return userPreferences;
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("Timeout error while getting user preferences: {ex}", ex);
                throw; // or handle accordingly
            }
            catch (MongoException ex)
            {
                _logger.LogError("MongoDB error while getting user preferences: {ex}", ex);
                throw; // or handle accordingly
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error while getting user preferences: {ex}", ex);
                throw; // or handle accordingly
            }
        }

        internal async Task AddUserPreference(UserPreferenceAddRequest userPreferences)
        {
            try
            {
                _logger.LogInformation("Entering AddUserPreference inside UserPreferencesDbService");
                UserPreferences newUserPreferences = new()
                {
                    UserId = userPreferences.UserId,
                    Categories = userPreferences.Categories,
                    CommunicationChannel = userPreferences.CommunicationChannel
                };
                await _db.UserPreferencesCollection.InsertOneAsync(newUserPreferences);
                _logger.LogInformation("User preferences added in UserPreferencesDbService");
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("Timeout error while adding user preferences: {ex}", ex);
                throw; // or handle accordingly
            }
            catch (MongoException ex)
            {
                _logger.LogError("MongoDB error while adding user preferences: {ex}", ex);
                throw; // or handle accordingly
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error while adding user preferences: {ex}", ex);
                throw; // or handle accordingly
            }
        }

        internal async Task DeleteUserPreference(string id)
        {
            try
            {
                _logger.LogInformation("Entering DeleteUserPreference inside UserPreferencesDbService");
                await _db.UserPreferencesCollection.DeleteOneAsync(x => x.UserId == id);
                _logger.LogInformation("User preferences deleted in UserPreferencesDbService");
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("Timeout error while deleting user preferences: {ex}", ex);
                throw;
            }
            catch (MongoException ex)
            {
                _logger.LogError("MongoDB error while deleting user preferences: {ex}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error while deleting user preferences: {ex}", ex);
                throw;
            }
        }

        internal async Task UpdateUserPreferences(UserPreferenceUpdateRequest userPreferences)
        {
            try
            {
                _logger.LogInformation("Entering UpdateUserPreferences inside UserPreferencesDbService");
                var filter = Builders<UserPreferences>.Filter.Eq(x => x.UserId, userPreferences.UserId);
                var update = Builders<UserPreferences>.Update
                    .Set(x => x.Categories, userPreferences.Categories)
                    .Set(x => x.CommunicationChannel, userPreferences.CommunicationChannel);
                await _db.UserPreferencesCollection.UpdateOneAsync(filter, update);
                _logger.LogInformation("User preferences updated in UserPreferencesDbService");
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("Timeout error while updating user preferences: {ex}", ex);
                throw;
            }
            catch (MongoException ex)
            {
                _logger.LogError("MongoDB error while updating user preferences: {ex}", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error while updating user preferences: {ex}", ex);
                throw;
            }
        }
    }
}
