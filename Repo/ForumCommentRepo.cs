using LmsApi.Config;
using LmsApi.IRepo;
using LmsApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LmsApi.Repo;

public class ForumCommentRepo : IForumCommentRepo
{
    readonly DBContextConfig _context;

    public ForumCommentRepo(DBContextConfig context)
    {
        _context = context;
    }

    public List<ForumComment> GetForumCommentListByForum(int forumId)
    {
        var commentList = _context.ForumComments
                        .Where(fc => fc.ForumId == forumId)
                        .OrderBy(fc => fc.CreatedAt)
                        .Include(fc => fc.User)
                        .ToList();
        return commentList;
    }
}
