using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using TheNewsReporter.Accessors.AIAssistentService.Models;
using TheNewsReporter.Accessors.AIAssistentService.Models.Articles;
using TheNewsReporter.Accessors.AIAssistentService.Models.Requests;
using TheNewsReporter.Accessors.AIAssistentService.Models.Responses;

namespace TheNewsReporter.Accessors.AIAssistentService.Services
{
    public class AIAssistantService
    {
        private readonly ILogger<AIAssistantService> _logger;
        private readonly HttpClient _httpClient;

        private readonly string _url;

        private StringBuilder _stringBuilder;

        public AIAssistantService(IOptions<AIApiSettings> options,ILogger<AIAssistantService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;

            _url = options.Value.ApiUrl + options.Value.ApiKey;

            _stringBuilder = new StringBuilder();
        }

        public async Task<AIAssistantRecommededResponse> GetRecommendedNews(AIAssistantRequest query)
        {
            _logger.LogInformation("Recommending news in Service");
            try
            {
                var prompt = BuildRecommendedPrompt(query);
                var aiResponseText = await SendRequestToAI(prompt);

                _logger.LogInformation("Text response from AI: {aiResponseText}", aiResponseText);

                var recommendations = JsonSerializer.Deserialize<AIAssistantRecommededResponse>(aiResponseText);

                _logger.LogInformation("Recomendations are: {recommendations}",JsonSerializer.Serialize(recommendations));

                _logger.LogInformation("Returning recommended news in Service");
                return recommendations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while recommending news");
                throw;
            }
        }

        public async Task<AIAssistantSummerizedResponse> GetSummarizedNews(AIAssistantRequest query)
        {
            _logger.LogInformation("Summarizing news in Service");
            try
            {
                var prompt = BuildSummarizedPrompt(query);
                var aiResponseText = await SendRequestToAI(prompt);

                _logger.LogInformation("Text response from AI: {aiResponseText}", aiResponseText);

                var summerized = JsonSerializer.Deserialize<AIAssistantSummerizedResponse>(aiResponseText);

                _logger.LogInformation("Recomendations are: {recommendations}", JsonSerializer.Serialize(summerized));

                _logger.LogInformation("Returning Summarized news in Service");
                return summerized;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while summarizing news");
                throw;
            }
        }

        public async Task<AIAssistantRecAndSumResponse> GetRecommendedAndSummarizedNews(AIAssistantRequest query)
        {
            _logger.LogInformation("Recommending and summarizing news in Service");
            try
            {
                var prompt = BuildRecommendAndSummarizePrompt(query);
                var aiResponseText = await SendRequestToAI(prompt);

                _logger.LogInformation("Text response from AI: {aiResponseText}", aiResponseText);

                var results = JsonSerializer.Deserialize<AIAssistantRecAndSumResponse>(aiResponseText);

                _logger.LogInformation("Recomendations are: {recommendations}", JsonSerializer.Serialize(results));

                _logger.LogInformation("Returning recommended and summarized news in Service");
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while recommending and summarizing news");
                throw;
            }
        }

        private async Task<string> SendRequestToAI(string prompt)
        {
            _logger.LogInformation("Sending request to AI");
            try
            {
                var requestBody = new
                {
                    contents = new[]
                    {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = prompt
                        }
                    }
                }
            }
                };

                var response = await _httpClient.PostAsJsonAsync(_url, requestBody);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Received response from AI: {responseContent}", responseContent);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                try
                {
                    var aiResponse = JsonSerializer.Deserialize<AIResponse>(responseContent, options);

                    // Check if aiResponse and its nested properties are not null
                    if (aiResponse?.Candidates != null && aiResponse.Candidates.Count > 0 &&
                        aiResponse.Candidates[0].Content?.Parts != null && aiResponse.Candidates[0].Content.Parts.Count > 0)
                    {
                        return aiResponse.Candidates[0].Content.Parts[0].Text.Replace("```json","").Replace("```", "").Trim();
                    }
                    else
                    {
                        _logger.LogError("AI response is null or incomplete");
                        return "Error: AI response is null or incomplete.";
                    }
                }
                catch (JsonException jsonEx)
                {
                    _logger.LogError(jsonEx, "JSON deserialization error");
                    _logger.LogError("Response content: {responseContent}", responseContent);
                    return "Error: JSON deserialization error.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while sending request to AI");
                throw;
            }
        }

        private string BuildRecommendedPrompt(AIAssistantRequest query)
        {
            _stringBuilder.Clear();

            _stringBuilder.AppendLine("You are an assistant. Your job is as follows:");
            _stringBuilder.AppendLine("1. Recommend news articles based on the user's preferences.");
            _stringBuilder.AppendLine("2. Here are the articles provided:");

            AddArticlesToStringBuilder(query.Articles);

            _stringBuilder.AppendLine("3. The user's interests are:");
            AddUserIntrestsToStringBuilder(query);

            _stringBuilder.AppendLine("4. The kind of news the user prefers:");
            AddUserNewsPreferencesToStringBuilder(query);

            _stringBuilder.AppendLine("5. Based on these articles and the user's preferences, recommend news articles.");
            _stringBuilder.AppendLine("6. If user preferences are empty, recommend the news articles based on the articles provided.");
            _stringBuilder.AppendLine("7. Return the recommendations in the following JSON format:");
            _stringBuilder.AppendLine("{\"Result\": [{\"Title\": \"\", \"Description\": \"\", \"Link\": \"\"}]}");
            _stringBuilder.AppendLine("8. Ensure recommendations align with user preferences, but always include at least one interesting article if there's a mismatch.");
            _stringBuilder.AppendLine("9. In any case, always return as instructed with at least one empty object inside if no recomendations given if there is a recomendation dont add extra empty object. Without Extra explnations");
            return _stringBuilder.ToString();
        }

        private string BuildSummarizedPrompt(AIAssistantRequest query)
        {
            _stringBuilder.Clear();

            _stringBuilder.AppendLine("You are an assistant. Your job is as follows:");
            _stringBuilder.AppendLine("1. Summarize the news articles provided based on the user's preferences.");
            _stringBuilder.AppendLine("2. Here are the articles provided:");

            AddArticlesToStringBuilder(query.Articles);

            _stringBuilder.AppendLine("3. The user's interests are:");
            AddUserIntrestsToStringBuilder(query);

            _stringBuilder.AppendLine("4. The kind of news the user prefers are:");
            AddUserNewsPreferencesToStringBuilder(query);

            _stringBuilder.AppendLine("5. Based on these articles and the user's preferences, provide a summary.");
            _stringBuilder.AppendLine("6. Return the summary in the following JSON format:");
            _stringBuilder.AppendLine("{\"Result\": [{\"Title\": \"\", \"Link\": \"\", \"Summary\": \"\"}]}");
            _stringBuilder.AppendLine("7. If User preferences are empty, summarize and recommend the news articles based on the articles provided.");
            _stringBuilder.AppendLine("8. If User Prefernces/Categories Don't align with the articles, Use your own discretion, Better To to return something.");
            _stringBuilder.AppendLine("9. In any Case Must always Return as instructed.");

            return _stringBuilder.ToString();
        }

        private string BuildRecommendAndSummarizePrompt(AIAssistantRequest query)
        {
            _stringBuilder.Clear();

            _stringBuilder.AppendLine("You are an assistant. Your job is as follows:");
            _stringBuilder.AppendLine("1. Recommend and summarize news articles based on the user's preferences.");
            _stringBuilder.AppendLine("2. Here are the articles provided:");
            
            AddArticlesToStringBuilder(query.Articles);

            _stringBuilder.AppendLine("3. Based on these articles and the user's preferences, recommend and the ones you recommend summarize those news articles.");
            _stringBuilder.AppendLine("4. Return the recommendations and summaries in the following JSON format:");
            _stringBuilder.AppendLine("{\"Result\": [{\"Title\": \"\", \"Description\": \"\", \"Link\": \"\", \"Summary\": \"\"}]}");
            _stringBuilder.AppendLine("7. If User preferences are empty, summarize and recommend the news articles based on the articles provided.");
            _stringBuilder.AppendLine("8. If User Prefernces/Categories Don't align with the articles, Use your own discretion, Better To to return something.");
            _stringBuilder.AppendLine("9. In any Case Must always Return as instructed in .");

            return _stringBuilder.ToString();
        }

        private void AddArticlesToStringBuilder(List<NewsArticle> articles)
        {
            foreach (var article in articles)
            {
                _stringBuilder.AppendLine($"- Title: {article.Title}");
                _stringBuilder.AppendLine($"  Description: {article.Description}");
                _stringBuilder.AppendLine($"  Link: {article.Link}");
            }
        }

        private void AddUserNewsPreferencesToStringBuilder(AIAssistantRequest query)
        {
            foreach (var preference in query.UserPreferences.NewsPreferences)
            {
                _stringBuilder.AppendLine($"- {preference}");
            }
        }

        private void AddUserIntrestsToStringBuilder(AIAssistantRequest query)
        {
            foreach (var interest in query.UserPreferences.Categories)
            {
                _stringBuilder.AppendLine($"- {interest}");
            }
        }
    }
}
