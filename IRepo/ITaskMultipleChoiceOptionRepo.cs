using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ITaskMultipleChoiceOptionRepo
{
    TaskMultipleChoiceOption CreateOption(TaskMultipleChoiceOption option);
    List<TaskMultipleChoiceOption> GetMultipleChoiceOptionListByQuestion(int questionId);
}
