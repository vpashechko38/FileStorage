namespace FileStorage.Domain.Dtos;

public class FileInfoDto
{
    public Stream Stream { get; set; }

    public string ContentType { get; set; }

    public string FileName { get; set; }
}