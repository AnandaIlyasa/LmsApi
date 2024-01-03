using LmsApi.Config;
using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ILMSFileRepo
{
    LMSFile GetFileById(int id);
}
