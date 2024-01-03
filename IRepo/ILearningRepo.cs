using LmsApi.Model;

namespace LmsApi.IRepo;

public interface ILearningRepo
{
    List<Learning> GetLearningListByClass(int classId);
}
