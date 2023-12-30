using LmsApi.Config;

using LmsApi.IRepo;
using LmsApi.Model;

namespace LmsApi.Repo;

public class StudentClassRepo : IStudentClassRepo
{
    readonly DBContextConfig _context;

    public StudentClassRepo(DBContextConfig context)
    {
        _context = context;
    }

    public StudentClass CreateNewStudentClass(StudentClass studentClass)
    {
        _context.StudentClasses.Add(studentClass);
        _context.SaveChanges();
        return studentClass;
    }
}
