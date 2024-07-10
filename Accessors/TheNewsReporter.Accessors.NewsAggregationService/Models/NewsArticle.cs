﻿using System.Text.Json.Serialization;

namespace TheNewsReporter.Accessors.NewsAggregationService.Models
{
    public class NewsArticle
    {
        [JsonPropertyName("article_id")]
        public string ArticleId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public List<string> Keywords { get; set; }
        public List<string> Creator { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string PubDate { get; set; }
        public string Language { get; set; }
        public List<string> Country { get; set; }
        public List<string> Category { get; set; }
    }
}
