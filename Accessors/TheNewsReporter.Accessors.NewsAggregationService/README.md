# News Aggregation Service

The News Aggregation Service is an ASP.NET Core Web API application designed to fetch and aggregate news articles from a third-party API based on specified categories and parameters.

## Table of Contents

- [Setup](#setup)
  - [Prerequisites](#prerequisites)
- [Configuration](#configuration)
  - [News API Settings](#news-api-settings)
- [Usage](#usage)
  - [Endpoints](#endpoints)
    - [GET /api/newsaggregation/latest-news](#get-apinewsaggregationlatest-news)
    - [GET /api/newsaggregation/latest-news-by-category](#get-apinewsaggregationlatest-news-by-category)
  - [Example Response](#example-response)
- [Additional Notes](#additional-notes)

## Setup

### Prerequisites

Before running the News Aggregation Service, ensure you have the following installed:

- .NET 8 SDK or later
- Visual Studio IDE (optional, for development)
- Docker (optional, for containerization)

## Configuration

### News API Settings

The service requires configuration settings for the News API. These settings are managed in the `appsettings.json` file or environment variables. Ensure the following settings are configured:

```json
{
  "NewsApiSettings": {
    "BaseUrl": "https://newsdata.io/api/1",
    "ApiKey": "your_api_key_here",
    "PageSize": 10
  }
}
```

Replace **your_api_key_here** with your actual News API key.
Replace **PageSize** with your tiers Max Page Size

## Usage

### Endpoints

#### GET /api/newsaggregation/latest-news

Fetches the latest news articles.

**Request:**

- Query Parameters:
  - `size` (optional): The number of articles to fetch. Default is 10.
  - `language` (optional): The language of the articles. Default is "en".

**Response:**

- `200 OK`: Returns a list of news articles.
- `500 Internal Server Error`: If an error occurs while fetching the news.

**Example:**

```sh
GET /api/newsaggregation/latest-news?size=5&language=en

```

#### GET /api/newsaggregation/latest-news-by-category

Fetches the latest news articles by specified categories.

**Request:**

- Query Parameters:
  - `categories` (required): A list of categories to filter the news.
  - `sizepercategory` (optional): The number of articles to fetch per category. Default is 10.
  - `language` (optional): The language of the articles. Default is "en".

**Response:**

- `200 OK`: Returns a list of news articles filtered by categories.
- `500 Internal Server Error`: If an error occurs while fetching the news.

**Example:**

```sh
GET /api/newsaggregation/latest-news-by-category?categories=politics,world&sizepercategory=5&language=en
```

### Example Response

Here is an example JSON response for a `NewsArticle` object:

```json
{
  "article_id": "123456",
  "title": "Breaking News: Major Event Happens",
  "link": "https://example.com/breaking-news-major-event-happens",
  "keywords": ["Breaking News", "Major Event"],
  "creator": ["Jane Doe", "John Smith"],
  "description": "A major event has just happened, and it is breaking news.",
  "content": "In-depth content about the major event that just happened.",
  "pub_date": "2024-07-10T12:34:56Z",
  "language": "english",
  "country": ["US", "CA"],
  "category": ["Politics", "World"]
}
```

## Additional Notes

- Ensure CORS is configured correctly if the API is accessed from a different domain.
- Use the Swagger UI for API documentation and testing. It is available at `/swagger` when running in the development environment.
