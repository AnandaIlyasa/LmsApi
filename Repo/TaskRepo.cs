using LmsApi.Config;
using LmsApi.IRepo;
using LmsApi.Model;

namespace LmsApi.Repo;

public class TaskRepo : ITaskRepo
{
    DBContextConfig _context { get; set; }

    public TaskRepo(DBContextConfig context)
    {
        _context = context;
    }

    public LMSTask CreateTask(LMSTask task)
    {
        _context.LMSTasks.Add(task);
        _context.SaveChanges();
        return task;
    }
}
