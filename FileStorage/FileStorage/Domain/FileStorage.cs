using FileStorage.Data;
using FileStorage.Domain.Dtos;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace FileStorage.Domain;

public class FileStorage : IFileStorage
{
    private readonly GridFSBucket _bucket;

    public FileStorage(FileContext fileContext)
    {
        _bucket = new GridFSBucket(fileContext.Database, new GridFSBucketOptions
        {
            BucketName = "pictures",
            ChunkSizeBytes = 1048576, // 1MB
            WriteConcern = WriteConcern.WMajority,
            ReadPreference = ReadPreference.Secondary
        });
    }

    public async Task<string> UploadAsync(IFormFile file)
    {
        var options = new GridFSUploadOptions
        {
            Metadata = new BsonDocument
            {
                {"ContentDisposition", file.ContentDisposition},
                {"ContentType", file.ContentType},
            }
        };
        var id = await _bucket.UploadFromStreamAsync(file.FileName, file.OpenReadStream(), options);
        return id.ToString();
    }

    public async Task<FileInfoDto> DownloadAsync(string id)
    {
        const string contentType = "ContentType";
        
        var oid = ObjectId.Parse(id);
        var file = await _bucket.OpenDownloadStreamAsync(oid);
        return new FileInfoDto
        {
            Stream = file,
            ContentType = file.FileInfo.Metadata[contentType].AsString,
            FileName = file.FileInfo.Filename
        };
    }
}