using ms17.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms17.BLL.Interface
{
    public interface ICommentService
    {
        Comment FindCommentById(int ID);
        IQueryable<Comment> Comments { get; }
        bool DeleteCommentById(int commentId, out int photoId);
        bool CreateComment(Comment comment);
    }
}
