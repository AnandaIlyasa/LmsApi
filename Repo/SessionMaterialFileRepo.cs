using LmsApi.Config;
using LmsApi.IRepo;
using LmsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LmsApi.Repo;

public class SessionMaterialFileRepo : ISessionMaterialFileRepo
{
    readonly DBContextConfig _context;

    public SessionMaterialFileRepo(DBContextConfig context)
    {
        _context = context;
    }

    public SessionMaterialFile CreateMaterialFile(SessionMaterialFile materialFile)
    {
        _context.SessionMaterialFiles.Add(materialFile);
        _context.SaveChanges();
        return materialFile;
    }

    public List<SessionMaterialFile> GetSessionMaterialFileListByMaterial(int materialId)
    {
        var materialFileList = _context.SessionMaterialFiles
                                .Where(mf => mf.MaterialId == materialId)
                                .Include(mf => mf.File)
                                .ToList();
        return materialFileList;
    }
}
