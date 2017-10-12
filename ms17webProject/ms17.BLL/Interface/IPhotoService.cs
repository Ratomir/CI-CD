using ms17.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ms17.BLL.Interface
{
    public interface IPhotoService
    {
        Photo FindPhotoById(int ID);
        bool CreatePhoto(Photo newPhoto, HttpPostedFileBase image);
        Photo FindPhotoByTitle(string Title);
        bool DeletePhotoById(int photoId);
        IQueryable<Photo> Photos { get; }
    }
}
