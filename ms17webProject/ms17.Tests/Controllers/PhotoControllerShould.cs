using System;
using ms17.Controllers;
using Moq;
using ms17.BLL.Interface;
using ms17.DAL;
using Xunit;
using System.Web.Mvc;

namespace ms17.Web.Tests.Controllers
{
    public class PhotoControllerShould
    {
        private readonly Mock<IPhotoService> _photoService;
        private readonly PhotoController _sut;

        public PhotoControllerShould()
        {
            _photoService = new Mock<IPhotoService>();

            _photoService.Setup(t => t.DeletePhotoById(It.IsAny<int>())).Returns(true);
            _photoService.Setup(t => t.FindPhotoById(It.IsAny<int>())).Returns(new Photo()
            {

                Comments = null,
                CreatedDate = new DateTime(),
                Description = "Test description",
                ImageMimeType = "jpg",
                PhotoFile = new byte[4500],
                PhotoID = 45,
                Title = "Test title 1",
                UserName = "test username"
            });

            _photoService.Setup(t => t.CreatePhoto(It.IsAny<Photo>(), null)).Returns(true);

            _sut = new PhotoController(_photoService.Object);
        }

        [Fact]
        public void DeletePhotoById()
        {
            ActionResult ar = _sut.DeleteConfirmed(34);
            RedirectToRouteResult routeResult = Assert.IsType<RedirectToRouteResult>(ar);
            Assert.Equal("Index", routeResult.RouteValues["action"]);
        }

        [Fact]
        public void ReturnViewWhenIsInvalidModelState()
        {
            _sut.ModelState.AddModelError("Title", "Test error");

            Photo newPhoto = new Photo()
            {
                Comments = null,
                CreatedDate = new DateTime(),
                Description = "Test description",
                ImageMimeType = "jpg",
                PhotoFile = new byte[4500],
                PhotoID = 45,
                UserName = "test username"
            };

            ActionResult ar = _sut.Create(newPhoto, null);

            ViewResult viewResult = Assert.IsType<ViewResult>(ar);

            Photo modelFromAction = Assert.IsType<Photo>(viewResult.Model);

            Assert.Equal(newPhoto.UserName, modelFromAction.UserName);
        }

        [Fact]
        public void ReturnIndexViewWhenModelStateIsValid()
        {
            Photo newPhoto = new Photo()
            {
                Comments = null,
                Title = "New title",
                CreatedDate = new DateTime(),
                Description = "Test description",
                ImageMimeType = "jpg",
                PhotoFile = new byte[4500],
                UserName = "test username"
            };

            ActionResult ar = _sut.Create(newPhoto, null);

            RedirectToRouteResult viewResult = Assert.IsType<RedirectToRouteResult>(ar);

            RedirectToRouteResult routeResult = Assert.IsType<RedirectToRouteResult>(ar);
            Assert.Equal("Index", routeResult.RouteValues["action"]);
        }
    }
}
