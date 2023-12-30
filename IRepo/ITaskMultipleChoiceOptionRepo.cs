using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ITaskMultipleChoiceOptionRepo
{
    List<TaskMultipleChoiceOption> GetMultipleChoiceOptionListByQuestion(int questionId);
}
