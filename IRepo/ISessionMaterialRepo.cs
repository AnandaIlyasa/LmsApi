using LmsApi.Config;
using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ISessionMaterialRepo
{
    SessionMaterial CreateSessionMaterial(SessionMaterial material);
    List<SessionMaterial> GetMaterialListBySession(int sessionId);
}
