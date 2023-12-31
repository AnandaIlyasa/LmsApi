using LmsApi.IRepo;
using LmsApi.IService;
using LmsApi.Model;

namespace LmsApi.Service;

public class FileService : IFileService
{
    readonly ILMSFileRepo _fileRepo;

    public FileService(ILMSFileRepo fileRepo)
    {
        _fileRepo = fileRepo;
    }

    public LMSFile GetFileById(int id)
    {
        var response = _fileRepo.GetFileById(id);
        return response;
    }
}
