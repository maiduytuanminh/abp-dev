using System.Threading.Tasks;
using SmartSoftware.Application.Services;
using SmartSoftware.Content;

namespace SmartSoftware.Blogging.Files
{
    public interface IFileAppService : IApplicationService
    {
        Task<RawFileDto> GetAsync(string name);

        Task<IRemoteStreamContent> GetFileAsync(string name);

        Task<FileUploadOutputDto> CreateAsync(FileUploadInputDto input);
    }
}
