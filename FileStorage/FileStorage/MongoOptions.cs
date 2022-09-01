namespace FileStorage;

public class MongoOptions
{
    public const string DefaultSection = "mongoDb";
    
    public string ConnectionString { get; set; }

    public string Database { get; set; }
}