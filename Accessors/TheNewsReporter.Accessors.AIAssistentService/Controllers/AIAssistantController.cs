using Dapr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheNewsReporter.Accessors.AIAssistentService.Models.Requests;
using TheNewsReporter.Accessors.AIAssistentService.Models.Responses;
using TheNewsReporter.Accessors.AIAssistentService.Services;

namespace TheNewsReporter.Accessors.AIAssistentService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AIAssistantController : ControllerBase
    {
        private readonly ILogger<AIAssistantController> _logger;
        private readonly AIAssistantService _aiAssistantService;

        public AIAssistantController(ILogger<AIAssistantController> logger, AIAssistantService aiAssistantService)
        {
            _logger = logger;
            _aiAssistantService = aiAssistantService;
        }

        [HttpPost("recommend")]
        public async Task<ActionResult<AIAssistantRecommededResponse>> RecommendNews([FromBody] AIAssistantRequest query)
        {
            _logger.LogInformation("Recommending news in Controller");
            try
            {
                var result = await _aiAssistantService.GetRecommendedNews(query);

                _logger.LogInformation("Returning recommended news in Controller");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while recommending news");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("summarize")]
        public async Task<ActionResult<AIAssistantSummerizedResponse>> SummarizeNews([FromBody] AIAssistantRequest query)
        {
            _logger.LogInformation("Summarizing news in Controller");
            try
            {
                var result = await _aiAssistantService.GetSummarizedNews(query);

                _logger.LogInformation("Returning Summarized news in Controller");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while summarizing news");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("/recommendandsummerizequeue")]
        public async Task<ActionResult<AIAssistantRecAndSumResponse>> RecommendAndSummarizeNews([FromBody]AIAssistantRequest query)
        {
            _logger.LogInformation("Recommending and summarizing news in Controller");
            try
            {
                var result = await _aiAssistantService.GetRecommendedAndSummarizedNews(query);

                _logger.LogInformation("Returning recommended and summarized news in Controller");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while recommending and summarizing news");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
