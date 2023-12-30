using LmsApi.Config;
using LmsApi.IRepo;
using LmsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LmsApi.Repo;

public class TaskQuestionRepo : ITaskQuestionRepo
{
    readonly DBContextConfig _context;

    public TaskQuestionRepo(DBContextConfig context)
    {
        _context = context;
    }

    public List<TaskQuestion> GetQuestionListByTask(int taskId)
    {
        var questionList = _context.TaskQuestions
                            .Join(
                                _context.TaskDetails,
                                q => q.Id,
                                td => td.TaskQuestionId,
                                (q, td) => new { q, td }
                            )
                            .Where(qtd => qtd.td.TaskId == taskId)
                            .Select(qtd => qtd.q)
                            .GroupBy(q => q.Id)
                            .Select(q => q.First())
                            .ToList();
        return questionList;
    }
}
