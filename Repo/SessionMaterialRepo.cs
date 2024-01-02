using LmsApi.Config;

using LmsApi.IRepo;
using LmsApi.Model;

namespace LmsApi.Repo;

public class SessionMaterialRepo : ISessionMaterialRepo
{
    readonly DBContextConfig _context;

    public SessionMaterialRepo(DBContextConfig context)
    {
        _context = context;
    }

    public SessionMaterial CreateSessionMaterial(SessionMaterial material)
    {
        _context.SessionMaterials.Add(material);
        _context.SaveChanges();
        return material;
    }

    public List<SessionMaterial> GetMaterialListBySession(int sessionId)
    {
        var materialList = _context.SessionMaterials
                        .Where(m => m.SessionId == sessionId)
                        .ToList();
        return materialList;
    }
}
