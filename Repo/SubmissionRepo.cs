using LmsApi.Config;
using LmsApi.IRepo;
using LmsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LmsApi.Repo;

public class SubmissionRepo : ISubmissionRepo
{
    readonly DBContextConfig _context;

    public SubmissionRepo(DBContextConfig context)
    {
        _context = context;
    }

    public Submission CreateNewSubmission(Submission submission)
    {
        _context.Submissions.Add(submission);
        _context.SaveChanges();
        return submission;
    }

    public Submission GetStudentSubmissionByTask(int studentId, int taskId)
    {
        var submission = _context.Submissions
                        .Where(s => s.StudentId == studentId && s.TaskId == taskId)
                        .First();
        return submission;
    }

    public List<Submission> GetStudentSubmissionListBySession(int sessionId, int studentId)
    {
        var query =
            from s in _context.Submissions
            join t in _context.LMSTasks on s.TaskId equals t.Id into stGroup
            from st in stGroup.DefaultIfEmpty()
            join ses in _context.Sessions on st.SessionId equals ses.Id into sesGroup
            where s.StudentId == studentId
            select s;

        var submissionList = query.ToList();
        return submissionList;
    }

    public List<Submission> GetSubmissionListByTask(int taskId)
    {
        var query =
            from s in _context.Submissions
            where s.TaskId == taskId
            orderby s.CreatedAt
            select s;

        var submissionList = query
                            .Include(s => s.Student)
                            .ToList();
        return submissionList;
    }

    public int UpdateSubmissionGradeAndNotes(Submission submission)
    {
        var foundSubmission = _context.Submissions
                            .Where(s => s.Id == submission.Id)
                            .First();
        foundSubmission.Grade = submission.Grade;
        foundSubmission.TeacherNotes = submission.TeacherNotes;
        return _context.SaveChanges();
    }
}
