using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ILearningRepo
{
    Learning CreateLearning(Learning learning);
    List<Learning> GetLearningListByClass(int classId);
}
