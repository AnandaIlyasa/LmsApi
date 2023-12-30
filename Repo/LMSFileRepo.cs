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

    public LMSFile CreateNewFile(LMSFile file)
    {
        _context.LMSFiles.Add(file);
        _context.SaveChanges();
        return file;
    }
}
