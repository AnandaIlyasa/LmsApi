using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ISessionMaterialFileRepo
{
    List<SessionMaterialFile> GetSessionMaterialFileListByMaterial(int materialId);
}
