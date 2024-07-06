# User Preferences Service

The User Preferences Service manages user-specific preferences using MongoDB as the data store. It provides endpoints for CRUD operations on user preferences, including retrieving, adding, updating, and deleting user preferences.

## Technologies Used

- **ASP.NET Core**: Framework for building APIs
- **MongoDB**: NoSQL database for storing user preferences
- **MongoDB.Driver**: Official MongoDB driver for .NET

### Prerequisites

- .NET SDK installed (version 8.X.X)
- MongoDB installed and running locally

## Dockerzation (optional)

- run build on Dockerfile or run localy

## Configure MongoDB Settings

```
// Inside appsetting.json Update MongoDatabase Key example:
"MongoDatabase":
        "MongoDatabase": {
        "ConnectionString": "mongodb://localhost:27017",
        "DatabaseName": "DBName",
        "CollectionName": "MyCollection"
```

## Build and Run:

- Restore packages and build the solution:

  ```
  dotnet restore
  dotnet build
  ```

- Run the application:
  ```
  dotnet run --project YTheNewsReporter.Accessors.UserPreferencesService.csproj
  ```

### Example response:

```
  {
    "id": "54897121311evgdsfvfasasd", // ObjectId as string
    "user_id": "u23eqweqw56454616f1f1r1", // ObjectId as string
    "categories": ["Tech"],
    "communication_channel": {
      "channel": "Email",
      "details": {
        "email": "example@example.com" // details according to channel
      }
    }
  }
```

## API Endpoints

Retrieve All User Preferences

- **GET** `/api/userpreferences/alluserpreferences`
  - Retrieves all user preferences stored in the database.
  - Returns:
    - Status Code `200 OK` on success with a JSON array of user preferences.
    - Status Code `503 Service Unavailable` if a timeout occurs.
    - Status Code `500 Internal Server Error` for database errors.
    - Status Code `400 Bad Request` for unexpected errors.

### Retrieve User Preferences by ID

- **GET** `/api/userpreferences/{id}`
  - Retrieves user preferences for a specific user identified by `{id}`.
  - Returns:
    - Status Code `200 OK` on success with JSON representation of user preferences.
    - Status Code `503 Service Unavailable` if a timeout occurs.
    - Status Code `500 Internal Server Error` for database errors.
    - Status Code `400 Bad Request` for unexpected errors.

### Add User Preferences

- **POST** `/api/userpreferences/add`
  - Adds new user preferences for a user.
  - Expects a JSON body (`UserPreferenceAddRequest`) with `user_id`, `categories`, and `communication_channel`.
  - Returns:
    - Status Code `200 OK` on success with a success message.
    - Status Code `503 Service Unavailable` if a timeout occurs.
    - Status Code `500 Internal Server Error` for database errors.
    - Status Code `400 Bad Request` for unexpected errors.

### Add Random User Preferences

- **POST** `/api/userpreferences/addrandom`
  - Adds random user preferences for testing purposes.
  - Generates random `user_id`, `categories`, and `communication_channel`.
  - Returns:
    - Status Code `200 OK` on success with a success message.
    - Status Code `503 Service Unavailable` if a timeout occurs.
    - Status Code `500 Internal Server Error` for database errors.
    - Status Code `400 Bad Request` for unexpected errors.

### Delete User Preferences

- **DELETE** `/api/userpreferences/deleteuser/{id}`
  - Deletes user preferences for a specific user identified by `{id}`.
  - Returns:
    - Status Code `200 OK` on success with a success message.
    - Status Code `503 Service Unavailable` if a timeout occurs.
    - Status Code `500 Internal Server Error` for database errors.
    - Status Code `400 Bad Request` for unexpected errors.

### Update User Preferences

- **PUT** `/api/userpreferences/updateuserpreferences`
  - Updates user preferences for a user.
  - Expects a JSON body (`UserPreferenceUpdateRequest`) with `user_id`, `categories`, and `communication_channel`.
  - Returns:
    - Status Code `200 OK` on success with a success message.
    - Status Code `503 Service Unavailable` if a timeout occurs.
    - Status Code `500 Internal Server Error` for database errors.
    - Status Code `400 Bad Request` for unexpected errors.
