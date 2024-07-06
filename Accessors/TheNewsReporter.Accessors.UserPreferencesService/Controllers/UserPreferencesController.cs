using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TheNewsReporter.Accessors.UserPreferencesService.Models;
using TheNewsReporter.Accessors.UserPreferencesService.Services;

namespace TheNewsReporter.Accessors.UserPreferencesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPreferencesController : ControllerBase
    {
        private readonly ILogger<UserPreferencesController> _logger;
        private readonly UserPreferencesDbService _userPreferencesDbService;

        public UserPreferencesController(ILogger<UserPreferencesController> logger, UserPreferencesDbService userPreferencesService)
        {
            _logger = logger;
            _userPreferencesDbService = userPreferencesService;
        }

        #region Complete_preferences_operations
        [HttpGet("alluserpreferences")]
        public async Task<ActionResult<List<UserPreferences>>> GetAllUserPreferences()
        {
            try
            {
                _logger.LogInformation("Getting all user preferences");
                var userPreferences = await _userPreferencesDbService.GetAllUserPreferencesAsync();
                _logger.LogInformation("Returning all user preferences in controller");
                return Ok(userPreferences);
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("Timeout error while fetching all user preferences: {ex}", ex);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Service unavailable. Please try again later.");
            }
            catch (MongoException ex)
            {
                _logger.LogError("MongoDB error while fetching all user preferences: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database error occurred. Please contact support.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error while fetching all user preferences: {ex}", ex);
                return BadRequest("An unexpected error occurred. Please try again later.");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UserPreferences>> GetUserPreferences(string id)
        {
            try
            {
                _logger.LogInformation("Getting user preferences for user with id: {id}", id);
                var userPreferences = await _userPreferencesDbService.GetUserPreferences(id);
                _logger.LogInformation("Returning user preferences for user with id: {id}", id);
                return Ok(userPreferences);
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("Timeout error while fetching user preferences for user with id {id}: {ex}", id, ex);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Service unavailable. Please try again later.");
            }
            catch (MongoException ex)
            {
                _logger.LogError("MongoDB error while fetching user preferences for user with id {id}: {ex}", id, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database error occurred. Please contact support.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error while fetching user preferences for user with id {id}: {ex}", id, ex);
                return BadRequest("An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddUserPreference([FromBody] UserPreferenceAddRequest userPreferences)
        {
            try
            {
                _logger.LogInformation("Adding user preferences for user with id: {id}", userPreferences.UserId);
                await _userPreferencesDbService.AddUserPreference(userPreferences);
                _logger.LogInformation("User preferences added for user with id: {id}", userPreferences.UserId);
                return Ok("User Preferences Created");
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("Timeout error while adding user preferences for user with id {id}: {ex}", userPreferences.UserId, ex);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Service unavailable. Please try again later.");
            }
            catch (MongoException ex)
            {
                _logger.LogError("MongoDB error while adding user preferences for user with id {id}: {ex}", userPreferences.UserId, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database error occurred. Please contact support.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error while adding user preferences for user with id {id}: {ex}", userPreferences.UserId, ex);
                return BadRequest("An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("addrandom")]
        public async Task<ActionResult> AddRandomUserPreference()
        {
            try
            {
                _logger.LogInformation("Adding random user preferences");
                var userPreferences = new UserPreferenceAddRequest
                {
                    UserId = ObjectId.GenerateNewId().ToString(),
                    Categories = new List<string> { "Technology", "Science","Sports" },
                    CommunicationChannel = new CommunicationChannel
                    {
                        ChannelEnum = Channel.Email,
                        Details = new Dictionary<string, string> { { "email", "example@exa.com" } }
                    }
                };
                await _userPreferencesDbService.AddUserPreference(userPreferences);
                _logger.LogInformation("Random user preferences added");
                return Ok("Random User Added");
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("Timeout error while adding random user preferences: {ex}", ex);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Service unavailable. Please try again later.");
            }
            catch (MongoException ex)
            {
                _logger.LogError("MongoDB error while adding random user preferences: {ex}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database error occurred. Please contact support.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error while adding random user preferences: {ex}", ex);
                return BadRequest("An unexpected error occurred. Please try again later.");
            }
        }

        [HttpDelete("deleteuser/{id}")]
        public async Task<ActionResult> DeleteUserPreference(string id)
        {
            try
            {
                _logger.LogInformation("Deleting user preferences for user with id: {id}", id);
                await _userPreferencesDbService.DeleteUserPreference(id);
                _logger.LogInformation("User preferences deleted for user with id: {id}", id);
                return Ok("User Preferences Deleted");
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("Timeout error while deleting user preferences for user with id {id}: {ex}", id, ex);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Service unavailable. Please try again later.");
            }
            catch (MongoException ex)
            {
                _logger.LogError("MongoDB error while deleting user preferences for user with id {id}: {ex}", id, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database error occurred. Please contact support.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error while deleting user preferences for user with id {id}: {ex}", id, ex);
                return BadRequest("An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPut("updateuserpreferences")]
        public async Task<ActionResult> UpdateUserPreferences([FromBody] UserPreferenceUpdateRequest userPreferences)
        {
            try
            {
                _logger.LogInformation("Updating user preferences for user with id: {id}", userPreferences.UserId);
                await _userPreferencesDbService.UpdateUserPreferences(userPreferences);
                _logger.LogInformation("User preferences updated for user with id: {id}", userPreferences.UserId);
                return Ok("User Preferences Updated");
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("Timeout error while updating user preferences for user with id {id}: {ex}", userPreferences.UserId, ex);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Service unavailable. Please try again later.");
            }
            catch (MongoException ex)
            {
                _logger.LogError("MongoDB error while updating user preferences for user with id {id}: {ex}", userPreferences.UserId, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database error occurred. Please contact support.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error while updating user preferences for user with id {id}: {ex}", userPreferences.UserId, ex);
                return BadRequest("An unexpected error occurred. Please try again later.");
            }
        }

        #endregion
    }
}

