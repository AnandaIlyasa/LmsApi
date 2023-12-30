using LmsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LmsApi.Config;

public class DBContextConfig : DbContext
{
    public DbSet<Class> Classes { get; set; }
    public DbSet<Forum> Forums { get; set; }
    public DbSet<ForumComment> ForumComments { get; set; }
    public DbSet<Learning> Learnings { get; set; }
    public DbSet<LMSFile> LMSFiles { get; set; }
    public DbSet<LMSTask> LMSTasks { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<SessionAttendance> SessionAttendances { get; set; }
    public DbSet<SessionMaterial> SessionMaterials { get; set; }
    public DbSet<SessionMaterialFile> SessionMaterialFiles { get; set; }
    public DbSet<StudentClass> StudentClasses { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<SubmissionDetailQuestion> SubmissionDetailQuestions { get; set; }
    public DbSet<SubmissionDetailFile> SubmissionDetailFiles { get; set; }
    public DbSet<TaskDetail> TaskDetails { get; set; }
    public DbSet<TaskFile> TaskFiles { get; set; }
    public DbSet<TaskMultipleChoiceOption> TaskMultipleChoiceOptions { get; set; }
    public DbSet<TaskQuestion> TaskQuestions { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string host = "localhost";
        const string db = "LMS";
        const string username = "postgres";
        const string password = "postgres";
        const string connString = $"Host={host}; Database={db}; Username={username}; Password={password}";

        optionsBuilder.UseNpgsql(connString);
        //optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }
}
