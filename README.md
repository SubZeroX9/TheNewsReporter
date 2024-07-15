# The News Reporter API

The News Reporter API is a microservices-based application for fetching and aggregating news according to user preferences. It supports sending notifications through different communication channels such as email, etc.

## Table of Contents

- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Endpoints](#endpoints)
- [External APIs](#external-apis)
- [License](#license)

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [MongoDB](https://www.mongodb.com/try/download/community)
- [RabbitMQ](https://www.rabbitmq.com/download.html)
- [Dapr](https://dapr.io/)

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/SubZeroX9/TheNewsReporter.git
   cd TheNewsReporter
   ```

2. Change example.env in root to .env and update Variables
   <br>
3. Set up Docker containers:

   ```bash
   docker-compose up -d
   ```

4. Install the required .NET packages:

   ```bash
   dotnet restore
   ```

## Configuration

1. Change `.env.example` file in root to `.env` and update Variables

2. Update `docker-compose.yml` with your configurations.
   Examples: (Can Also Be found in `appsettings.json` of each service)

   - In AI Assistant Accessor

   ```yml
   thenewsreporter.accessors.aiassistentservice:
     environment:
       - AIApiSettings__ApiKey=${AI_API_KEY}
       - AIApiSettings__ApiUrl="https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key="
   ```

   - In User Preferences Accessor

   ```yml
   thenewsreporter.accessors.userpreferencesservice:
     environment:
       - MongoDatabase__ConnectionString=mongodb://mongodb-service:27017
       - MongoDatabase__DatabaseName="TheNewsReporter"
       - MongoDatabase__CollectionName="UserPreferences"
   ```

- In News Aggregation Accessor

  ```yml
  thenewsreporter.accessors.newsaggregationservice:
    environment:
      - NewsApiSettings__APIKey=${NEWS_API_KEY}
      - NewsApiSettings__BaseUrl="https://newsdata.io/api/1"
      - NewsApiSettings__PageSize=10
  ```

- In NotificationApi Accessor

  ```yml
  thenewsreporter.accessors.notificationservice:
    environment:
      - MailGunApiSettings__ApiKey=${MAILGUN_API_KEY}
      - MailGunApiSettings__BaseUrl="https://api.mailgun.net/v3"
      - MailGunApiSettings__Domain="sandboxcc3640df3a734f9281b2885ba77b09ce.mailgun.org"
      - MailGunApiSettings__DisplayName="The News Reporter"
  ```

## Usage

1. Start the API:

   ```bash
   dotnet run
   ```

2. Access the API documentation at `http://localhost:5006/` using Swagger.

## Endpoints

### User Preferences Service

- **GET /alluserpreferences**: Get all user preferences.
- **GET /{id}**: Get User Preferences by ID.
- **POST /setuserpreferencesqueue**: Create new user preferences.
- **PUT /updateuserpreferencesqueue**: Update user preferences by ID.
- **DELETE /deleteuser/{id}**: Delete user preferences by ID.

### News Aggregation Service

- **GET /api/NewsAggregation/latest-news**: Get latest News.
- **GET /api/NewsAggregation/latest-news-by-category**: Get latest news by category.

### Notification Service

- **POST /sendnotification**: Send a notification.(used by pubsub)

### AI Assistant Service

- **POST /AIAssistant/recommend**: Get recommended News articles based on preferences and articles from AI.
- **POST /AIAssistant/summarize**: Get summerized articles from provided articles by AI.
- **POST /recommendandsummerizequeue**: Get recommended and summerized News articles based on preferences and articles from AI.

### News Api Manager

- **POST /api/NewsApi/setpreferences**: Creates New Preferences for a user.
- **PUT /api/NewsApi/updateprefes**: Updates User Preferences.
- **GET api/NewsApi/getnews/{id}**: Get latest news for user id (based on preferences).

### Example Request

To get the latest news and process notifications for a user, make a `GET` request to:

```bash
curl -X GET "http://localhost:5006/api/newsaggregation/getnews/{id}" -H "accept: text/plain"
```

## External APIs

### Gemini AI

The Gemini AI service is used for enhancing the news articles with additional AI-generated insights.

- **Documentation**: [Gemini AI API Docs](https://docs.gemini.com/)

### MailGun

MailGun is used for sending email notifications to users.

- **Documentation**: [MailGun API Docs](https://documentation.mailgun.com/)

### NewsData.io

NewsData.io is used for fetching the latest news articles.

- **Documentation**: [NewsData.io API Docs](https://newsdata.io/docs/)

## Licence

This project is licensed under the MIT License - see the LICENSE.md file for details.
