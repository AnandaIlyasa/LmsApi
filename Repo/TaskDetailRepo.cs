using LmsApi.Config;
using LmsApi.IRepo;
using LmsApi.Model;

namespace LmsApi.Repo;

public class TaskDetailRepo : ITaskDetailRepo
{
    readonly DBContextConfig _context;

    public TaskDetailRepo(DBContextConfig context)
    {
        _context = context;
    }

    public TaskDetail CreateTaskDetail(TaskDetail taskDetail)
    {
        _context.TaskDetails.Add(taskDetail);
        _context.SaveChanges();
        return taskDetail;
    }
}
