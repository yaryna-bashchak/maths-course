using API.Repositories;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace API.Services;

public class ImageService
{
    private readonly Cloudinary _cloudinary;
    public ImageService(IConfiguration config)
    {
        var acc = new Account
        (
            config["Cloudinary:CloudName"],
            config["Cloudinary:ApiKey"],
            config["Cloudinary:ApiSecret"]
        );

        _cloudinary = new Cloudinary(acc);
    }

    public async Task<Result<bool>> ProcessImageAsync(IFormFile file, string existingPublicId, Action<string, string> update)
    {

        if (!string.IsNullOrEmpty(existingPublicId))
            await DeleteImageAsync(existingPublicId);

        var imageResult = await AddImageAsync(file);

        if (imageResult.Error != null)
            return new Result<bool> { IsSuccess = false, ErrorMessage = imageResult.Error.Message };

        update(imageResult.SecureUrl.ToString(), imageResult.PublicId);

        return null;
    }

    public async Task<ImageUploadResult> AddImageAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();

        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream)
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }

        return uploadResult;
    }

    public async Task<DeletionResult> DeleteImageAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(deleteParams);

        return result;
    }
}
