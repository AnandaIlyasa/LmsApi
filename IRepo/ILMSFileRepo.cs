using LmsApi.Config;
using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ILMSFileRepo
{
    LMSFile CreateNewFile(LMSFile file);
}
