namespace TheNewsReporter.Accessors.NewsAggregationService.Models
{
    public class NewsApiResponse
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public List<NewsArticle> Results { get; set; }
        public string NextPage { get; set; }
    }
}
