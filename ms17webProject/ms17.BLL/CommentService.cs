using ms17.BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ms17.DAL;

namespace ms17.BLL
{
    public class CommentService : ICommentService
    {
        private PhotoSharingContext _photoContext = null;

        public CommentService(PhotoSharingContext photoSharingContext)
        {
            _photoContext = photoSharingContext;
        }

        public IQueryable<Comment> Comments => _photoContext.Comments;

        public bool CreateComment(Comment comment)
        {
            //Save the new comment
            _photoContext.Comments.Add(comment);
            return _photoContext.SaveChanges() > 0;
        }

        public bool DeleteCommentById(int commentId, out int photoId)
        {
            Comment comment = FindCommentById(commentId);
            photoId = comment.PhotoID;
            _photoContext.Comments.Remove(comment);
            return _photoContext.SaveChanges() > 0;
        }

        public Comment FindCommentById(int ID)
        {
            try
            {
                return _photoContext.Comments.Find(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
