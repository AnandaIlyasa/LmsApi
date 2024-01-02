using LmsApi.Config;
using LmsApi.IRepo;
using LmsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LmsApi.Repo;

public class TaskFileRepo : ITaskFileRepo
{
    readonly DBContextConfig _context;

    public TaskFileRepo(DBContextConfig context)
    {
        _context = context;
    }

    public TaskFile CreateTaskFile(TaskFile taskFile)
    {
        _context.TaskFiles.Add(taskFile);
        _context.SaveChanges();
        return taskFile;
    }

    public List<TaskFile> GetTaskFileList(int taskId)
    {
        var taskFileList = _context.TaskFiles
                            .Join(
                                _context.TaskDetails,
                                tf => tf.Id,
                                td => td.TaskFileId,
                                (tf, td) => new { tf, td }
                            )
                            .Where(tftd => tftd.td.TaskId == taskId)
                            .Select(tftd => tftd.tf)
                            .ToList();
        return taskFileList;
    }
}
