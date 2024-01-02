using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ISessionMaterialFileRepo
{
    SessionMaterialFile CreateMaterialFile(SessionMaterialFile materialFile);
    List<SessionMaterialFile> GetSessionMaterialFileListByMaterial(int materialId);
}
