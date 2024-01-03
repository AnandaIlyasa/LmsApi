using LmsApi.Config;

using LmsApi.IRepo;
using LmsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LmsApi.Repo;

public class SubmissionDetailFileRepo : ISubmissionDetailFileRepo
{
    readonly DBContextConfig _context;

    public SubmissionDetailFileRepo(DBContextConfig context)
    {
        _context = context;
    }

    public List<SubmissionDetailFile> GetStudentSubmissionDetailFileByTask(int taskId, int studentId)
    {
        var submissionFileList = _context.SubmissionDetailFiles
                                    .Join(
                                        _context.Submissions,
                                        sdf => sdf.SubmissionId,
                                        s => s.Id,
                                        (sdf, s) => new { sdf, s }
                                    )
                                    .Where(sdfs => sdfs.s.TaskId == taskId && sdfs.s.StudentId == studentId)
                                    .Select(sdfs => sdfs.sdf)
                                    .Include(sdf => sdf.File)
                                    .ToList();
        return submissionFileList;
    }
}
