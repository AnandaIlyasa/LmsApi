using LmsApi.Config;
using LmsApi.Constant;

using LmsApi.IRepo;
using LmsApi.Model;
using System.Threading.Tasks;

namespace LmsApi.Repo;

public class SessionTaskRepo : ISessionTaskRepo
{
    readonly DBContextConfig _context;

    public SessionTaskRepo(DBContextConfig context)
    {
        _context = context;
    }

    public List<LMSTask> GetTaskListBySession(int sessionId)
    {
        //var sqlQuery =
        //    "SELECT " +
        //        "t.id, " +
        //        "t.task_name, " +
        //        "t.task_description, " +
        //        "t.duration, " +
        //        "tf.id AS task_file_id, " +
        //        "tf.file_name, " +
        //        "tff.file_content, " +
        //        "tff.file_extension, " +
        //        "q.id AS question_id, " +
        //        "q.question_type, " +
        //        "q.question_content, " +
        //        "mco.id AS option_id, " +
        //        "mco.option_char, " +
        //        "mco.option_text, " +
        //        "mco.is_correct " +
        //    "FROM " +
        //        "t_m_task t " +
        //    "LEFT JOIN " +
        //        "t_r_task_detail td ON t.id = td.task_id  " +
        //    "LEFT JOIN " +
        //        "t_r_task_file tf ON td.task_file_id = tf.id " +
        //    "LEFT JOIN " +
        //        "t_m_file tff ON tf.file_id = tff.id " +
        //    "LEFT JOIN " +
        //        "t_m_task_question q ON td.task_question_id = q.id " +
        //    "LEFT JOIN " +
        //        "t_m_task_multiple_choice_option mco ON q.id = mco.question_id " +
        //    "WHERE " +
        //        "t.session_id = @session_id";

        //var conn = DBHelper.GetConnection();
        //conn.Open();

        //var sqlCommand = conn.CreateCommand();
        //sqlCommand.CommandText = sqlQuery;
        //sqlCommand.Parameters.AddWithValue("@session_id", sessionId);
        //var reader = sqlCommand.ExecuteReader();
        //var taskList = new List<LMSTask>();
        //while (reader.Read())
        //{
        //    var tId = (int)reader["id"];
        //    var task = taskList.Find(t => t.Id == tId);
        //    if (task == null)
        //    {
        //        task = new LMSTask()
        //        {
        //            Id = tId,
        //            TaskName = (string)reader["task_name"],
        //            TaskDescription = reader["task_description"] is string ? (string)reader["task_description"] : null,
        //            Duration = (int)reader["duration"],
        //            TaskFileList = new List<TaskFile>(),
        //            TaskQuestionList = new List<TaskQuestion>(),
        //        };
        //        taskList.Add(task);
        //    }

        //    TaskFile? taskFile = null;
        //    if (reader["file_content"] is string)
        //    {
        //        var file = new LMSFile()
        //        {
        //            FileContent = (string)reader["file_content"],
        //            FileExtension = (string)reader["file_extension"],
        //        };
        //        taskFile = new TaskFile()
        //        {
        //            Id = (int)reader["task_file_id"],
        //            FileName = reader["file_name"] is string ? (string)reader["file_name"] : null,
        //            File = file,
        //        };
        //        task.TaskFileList.Add(taskFile);
        //    }
        //    else
        //    {
        //        var qId = (int)reader["question_id"];
        //        var taskQuestion = task.TaskQuestionList.Find(q => q.Id == qId);
        //        if (taskQuestion == null)
        //        {
        //            taskQuestion = new TaskQuestion()
        //            {
        //                Id = qId,
        //                QuestionType = (string)reader["question_type"],
        //                QuestionContent = (string)reader["question_content"],
        //                OptionList = new List<TaskMultipleChoiceOption>(),
        //            };
        //            task.TaskQuestionList.Add(taskQuestion);
        //        }

        //        if (taskQuestion.QuestionType == QuestionType.MultipleChoice)
        //        {
        //            var choiceOption = new TaskMultipleChoiceOption()
        //            {
        //                Id = (int)reader["option_id"],
        //                OptionChar = (string)reader["option_char"],
        //                OptionText = (string)reader["option_text"],
        //                IsCorrect = (bool)reader["is_correct"],
        //            };
        //            taskQuestion.OptionList.Add(choiceOption);
        //        }
        //    }
        //}

        //conn.Close();
        var taskList = _context.LMSTasks
                        .Where(t => t.SessionId == sessionId)
                        .ToList();
        return taskList;
    }
}
