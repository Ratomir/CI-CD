using ms17.BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ms17.DAL;
using System.Web;

namespace ms17.BLL
{
    public class PhotoService : IPhotoService
    {
        private PhotoSharingContext _photoContext = null;

        public PhotoService(PhotoSharingContext photoSharingContext)
        {
            _photoContext = photoSharingContext;
        }

        public IQueryable<Photo> Photos => _photoContext.Photos;

        public bool CreatePhoto(Photo photo, HttpPostedFileBase image)
        {
            photo.CreatedDate = DateTime.Today;

            if (image != null)
            {
                photo.ImageMimeType = image.ContentType;
                photo.PhotoFile = new byte[image.ContentLength];
                image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
            }
            _photoContext.Photos.Add(photo);
            return _photoContext.SaveChanges() > 0;
        }

        public bool DeletePhotoById(int photoId)
        {
            Photo photo = FindPhotoById(photoId);
            _photoContext.Photos.Remove(photo);
            return _photoContext.SaveChanges() > 0;
        }

        public Photo FindPhotoById(int ID)
        {
            try
            {
                return _photoContext.Photos.Single(t => t.PhotoID == ID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Photo FindPhotoByTitle(string Title)
        {
            try
            {
                return _photoContext.Photos.Where(t => t.Title==Title).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
