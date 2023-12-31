using LmsApi.Model;

namespace LmsApi.IService;

public interface IFileService
{
    LMSFile GetFileById(int id);
}
