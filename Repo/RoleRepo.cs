using LmsApi.Config;
using LmsApi.IRepo;
using LmsApi.Model;

namespace LmsApi.Repo;

public class RoleRepo : IRoleRepo
{
    DBContextConfig _context { get; set; }

    public RoleRepo(DBContextConfig context)
    {
        _context = context;
    }

    public Role GetRoleByCode(string roleCode)
    {
        var role = _context.Roles.Where(r => r.RoleCode == roleCode).First();
        return role;
    }
}
