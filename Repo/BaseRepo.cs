using LmsApi.Config;
using LmsApi.IService;
using LmsApi.Model;

namespace LmsApi.Repo;

public class BaseRepo
{
    readonly DBContextConfig _context;
    readonly IPrincipleService _principleService;

    public BaseRepo(DBContextConfig context, IPrincipleService principleService)
    {
        _context = context;
        _principleService = principleService;
    }

    public T CreateOrUpdateEntry<T>(T entry)
    {
        var casted = entry as BaseModel;
        if (casted.Id != 0)
        {
            casted.UpdatedBy = _principleService.GetLoginId();
            casted.UpdatedAt = DateTime.Now;
            _context.Update(casted);
        }
        else
        {
            if (casted.CreatedBy == 0) casted.CreatedBy = _principleService.GetLoginId();
            casted.CreatedAt = DateTime.Now;
            _context.Add(entry);
        }
        _context.SaveChanges();
        return entry;
    }
}
