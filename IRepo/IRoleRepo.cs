using LmsApi.Config;
using LmsApi.Model;

namespace LmsApi.IRepo;

public interface IRoleRepo
{
    Role GetRoleByCode(string roleCode);
}
