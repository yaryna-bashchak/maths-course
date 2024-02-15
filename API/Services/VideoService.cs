using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace API.Services;

public class VideoService
{
    private readonly Cloudinary _cloudinary;
    public VideoService(IConfiguration config)
    {
        var acc = new Account
        (
            config["Cloudinary:CloudName"],
            config["Cloudinary:ApiKey"],
            config["Cloudinary:ApiSecret"]
        );

        _cloudinary = new Cloudinary(acc);
    }

    public async Task<VideoUploadResult> AddVideoAsync(IFormFile file)
    {
        var uploadResult = new VideoUploadResult();

        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(file.FileName, stream)
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }

        return uploadResult;
    }

    public async Task<DeletionResult> DeleteVideoAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(deleteParams);

        return result;
    }
}
