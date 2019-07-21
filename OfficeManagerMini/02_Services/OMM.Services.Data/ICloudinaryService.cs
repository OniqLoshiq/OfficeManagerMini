using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OMM.Services.Data
{
    public interface ICloudinaryService
    {
        Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName);
    }
}
