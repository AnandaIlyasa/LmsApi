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

    public List<Learning> GetLearningListByClass(int classId)
    {
        var learningList = _context.Learnings
                        .Where(l => l.ClassId == classId)
                        .ToList();
        return learningList;
    }
}
