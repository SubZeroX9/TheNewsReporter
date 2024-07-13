using Dapr.Client;
using TheNewsReporter.Managers.NewsApiManager.Models.Requests;

namespace TheNewsReporter.Managers.NewsApiManager.Services
{
    public class AIAssistantService
    {
        private readonly ILogger<AIAssistantService> _logger;
        private readonly DaprClient _daprClient;

        public AIAssistantService(ILogger<AIAssistantService> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

    }
}
