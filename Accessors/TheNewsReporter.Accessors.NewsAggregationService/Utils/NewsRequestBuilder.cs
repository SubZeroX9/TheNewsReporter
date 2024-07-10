using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;

namespace TheNewsReporter.Accessors.NewsAggregationService.Utils
{
    public class NewsRequestBuilder
    {
        private readonly string _baseUrl;
        private readonly string _apiKey;

        private const string _apiKeyQuery = "apikey";
        private const string _sizeQuery = "size";
        private const string _categoryQuery = "category";
        private const string _pageQueary = "page";
        private const string _languageQuery = "language";

        private const string _LatestEndpoint = "latest";
        private const string _ArchiveEndpoint = "archive";

        private string _endpoint;
        private Dictionary<string, string> _queryParams;

        public NewsRequestBuilder(string baseUrl, string apiKey)
        {
            _baseUrl = baseUrl;
            _apiKey = apiKey;

            Reset();
        }

        public NewsRequestBuilder SetEndpointLatest()
        {
            _endpoint = _LatestEndpoint;
            return this;
        }

        public NewsRequestBuilder SetEndpointArchive()
        {
            _endpoint = _ArchiveEndpoint;
            return this;
        }

        public NewsRequestBuilder SetSize(int size)
        {
            _queryParams[_sizeQuery] = size.ToString();
            return this;
        }

        public NewsRequestBuilder SetCategory(string category)
        {
            if (string.IsNullOrEmpty(category) == true)
                return this;
            _queryParams[_categoryQuery] = category;
            return this;
        }

        public NewsRequestBuilder SetPage(string page)
        {
            if(string.IsNullOrEmpty(page) == true)
                return this;

            _queryParams[_pageQueary] = page;
            return this;
        }

        public NewsRequestBuilder SetLanguage(string language)
        {
            if (string.IsNullOrEmpty(language) == true)
                return this;
            _queryParams[_languageQuery] = language;
            return this;
        }

        public NewsRequestBuilder Reset()
        {
            _endpoint = _LatestEndpoint;
            _queryParams = new Dictionary<string, string>
            {
                { _apiKeyQuery, _apiKey }
            };
            return this;
        }

        public string BuildUrl()
        {
            var queryString = string.Join("&", _queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));

            return $"{ _baseUrl }/{ _endpoint }?{ queryString }";
        }
    }
}
