using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TheNewsReporter.Accessors.UserPreferencesService.Models;

public class DbContext
{
    public readonly IMongoCollection<UserPreferences> UserPreferencesCollection;

    public DbContext(IOptions<MongoDbSettings> options)
    {
        var _mongoClient = new MongoClient(options.Value.ConnectionString);
        UserPreferencesCollection = _mongoClient
            .GetDatabase(options.Value.DatabaseName)
            .GetCollection<UserPreferences>(options.Value.CollectionName);

        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexKeysDefinition = Builders<UserPreferences>.IndexKeys.Ascending(x => x.UserId);
        var indexModel = new CreateIndexModel<UserPreferences>(indexKeysDefinition, indexOptions);

        UserPreferencesCollection.Indexes.CreateOne(indexModel);
    }
}

