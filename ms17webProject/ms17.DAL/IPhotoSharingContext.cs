using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms17.DAL
{
    public interface IPhotoSharingContext
    {
        int SaveChanges();
        T Add<T>(T entity) where T : class;
        T Delete<T>(T entity) where T : class;



        //mitra dodao
        IQueryable<Photo> Photos { get; } 
        IQueryable<Comment> Comments { get; }

        Photo FindPhotoById(int ID);
        Photo FindPhotoByTitle(string Title);
        Comment FindCommentById(int ID);
    }
}
