using FileStorage.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IFileStorage _storage;

    public FileController(IFileStorage storage)
    {
        _storage = storage;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var id = await _storage.UploadAsync(file);
        return Ok(id);
    }

    [HttpGet("{fileId}")]
    public async Task<IActionResult> Download([FromRoute]string fileId)
    {
        var file = await _storage.DownloadAsync(fileId);
        return File(file.Stream, file.ContentType, file.FileName);
    }
}