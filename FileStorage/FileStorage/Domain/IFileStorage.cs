using FileStorage.Domain.Dtos;

namespace FileStorage.Domain;

public interface IFileStorage
{
    Task<string> UploadAsync(IFormFile file);
    Task<FileInfoDto> DownloadAsync(string id);
}