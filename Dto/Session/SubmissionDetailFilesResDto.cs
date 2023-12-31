using LmsApi.Dto.File;

namespace LmsApi.Dto.Session;

public class SubmissionDetailFilesResDto
{
    public int TaskFileId { get; set; }
    public List<int> FileIdList { get; set; }
}
