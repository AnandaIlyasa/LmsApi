using LmsApi.Config;
using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ISessionMaterialRepo
{
    List<SessionMaterial> GetMaterialListBySession(int sessionId);
}
