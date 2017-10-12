using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Globalization;
using ms17.DAL;
using ms17.BLL.Interface;

namespace ms17.Controllers
{
    [HandleError(View = "Error")]
    [ValueReporter]
    public class PhotoController : Controller
    {
        private IPhotoService _photoService;

        //Constructors
        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        //
        // GET: /Photo/
        public ActionResult Index()
        {
            return View("Index");
        }

        [ChildActionOnly]
        public ActionResult _PhotoGallery(int number = 0)
        {
            List<Photo> photos;

            if (number == 0)
            {
                photos = _photoService.Photos.ToList();
            }
            else
            {
                photos = (from p in _photoService.Photos
                          orderby p.CreatedDate descending
                          select p).Take(number).ToList();
            }

            return PartialView("_PhotoGallery", photos);
        }

        public ActionResult Display(int id)
        {
            Photo photo = _photoService.FindPhotoById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Display", photo);
        }

        public ActionResult DisplayByTitle(string title)
        {
            Photo photo = _photoService.FindPhotoByTitle(title);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Display", photo);
        }

        public ActionResult Create()
        {
            Photo newPhoto = new Photo();
            newPhoto.CreatedDate = DateTime.Today;
            return View("Create", newPhoto);
        }

        [HttpPost]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                bool createResult = _photoService.CreatePhoto(photo, image);
                if (createResult)
                {
                    return RedirectToAction("Index");
                }
            }

            return View("Create", photo);
        }

        public ActionResult Delete(int id)
        {
            Photo photo = _photoService.FindPhotoById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Delete", photo);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_photoService.DeletePhotoById(id))
            {
                return RedirectToAction("Index");
            }

            return HttpNotFound();
        }

        public FileContentResult GetImage(int id)
        {
            Photo photo = _photoService.FindPhotoById(id);
            if (photo != null)
            {
                return File(photo.PhotoFile, photo.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult SlideShow()
        {
            return View("SlideShow", _photoService.Photos.ToList());
        }
    }
}
