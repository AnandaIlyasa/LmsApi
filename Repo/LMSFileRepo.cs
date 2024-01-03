namespace LmsApi.Repo;

using LmsApi.Config;
using LmsApi.IRepo;
using LmsApi.Model;

public class LMSFileRepo : ILMSFileRepo
{
    readonly DBContextConfig _context;

    public LMSFileRepo(DBContextConfig context)
    {
        _context = context;
    }

    public LMSFile GetFileById(int id)
    {
        var file = _context.LMSFiles
                    .Where(f => f.Id == id)
                    .First();
        return file;
    }
}
