using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FileStorage.Data;

public class FileContext
{
    public IMongoDatabase Database;
    
    public FileContext(IOptions<MongoOptions> mongoConfig)
    {
        var client = new MongoClient(mongoConfig.Value.ConnectionString);
        Database = client.GetDatabase(mongoConfig.Value.Database);
    }
}