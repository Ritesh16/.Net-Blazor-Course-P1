using Microsoft.AspNetCore.Components.Forms;

namespace TangyWeb_Server.Services.Interfaces
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);
        bool DeleteFile(string filePath);
    }
}
