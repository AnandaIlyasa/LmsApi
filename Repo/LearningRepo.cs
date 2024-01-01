using LmsApi.Config;
using LmsApi.IRepo;
using LmsApi.Model;

namespace LmsApi.Repo;

public class LearningRepo : ILearningRepo
{
    readonly DBContextConfig _context;

    public LearningRepo(DBContextConfig context)
    {
        _context = context;
    }

    public Learning CreateLearning(Learning learning)
    {
        _context.Learnings.Add(learning);
        _context.SaveChanges();
        return learning;
    }

    public List<Learning> GetLearningListByClass(int classId)
    {
        var learningList = _context.Learnings
                        .Where(l => l.ClassId == classId)
                        .ToList();
        return learningList;
    }
}
