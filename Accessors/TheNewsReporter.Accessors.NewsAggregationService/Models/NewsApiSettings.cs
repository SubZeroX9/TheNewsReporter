namespace TheNewsReporter.Accessors.NewsAggregationService.Models
{
    public class NewsApiSettings
    {
        public string? ApiKey { get; set; }
        public string? BaseUrl { get; set; }
        public int PageSize { get; set; }
    }
}
