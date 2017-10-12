using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using ms17.DAL;
using System.Data.Entity;
using System.Threading.Tasks;
using ms17.BLL.Tests.Model;

namespace ms17.BLL.Tests
{
    /// <summary>
    /// Summary description for PhotoService
    /// </summary>
    public class PhotoServiceShould : BaseServiceTest
    {
        public PhotoServiceShould()
        {
            List<Photo> lstPhoto = new List<Photo>()
            {
                new Photo()
                {
                    Comments = null,
                    CreatedDate = new DateTime(),
                    Description = "Test description",
                    ImageMimeType = "jpg",
                    PhotoFile = new byte[4500],
                    PhotoID = 45,
                    Title = "Test title 1",
                    UserName = "test username"
                },
                new Photo()
                {
                    Comments = null,
                    CreatedDate = new DateTime(),
                    Description = "Test description",
                    ImageMimeType = "jpg",
                    PhotoFile = new byte[4500],
                    PhotoID = 75,
                    Title = "Test title 2",
                    UserName = "test username"
                }
            };

            Mock<DbSet<Photo>> mockPhoto = new Mock<DbSet<Photo>>().SetupData(lstPhoto);

            _mockContext.Setup(p => p.Photos).Returns(mockPhoto.Object);
        }

        [Theory]
        [InlineData(45)]
        [InlineData(75)]
        public void GetPhotoById(int income)
        {
            PhotoService sut = new PhotoService(_mockContext.Object);

            Photo result = sut.FindPhotoById(income);

            Assert.Equal(result.PhotoID, income);
        }

        [Fact]
        public async Task GetAllPhotos()
        {
            PhotoService sut = new PhotoService(_mockContext.Object);

            int count = await sut.Photos.CountAsync();

            Assert.Equal(count, 2);
        }

        [Theory]
        [InlineData("Test title 1")]
        public void GetPhotoByTitle(string income)
        {
            PhotoService sut = new PhotoService(_mockContext.Object);

            Photo result = sut.FindPhotoByTitle(income);

            Assert.Equal(result.PhotoID, 45);
        }

        [Fact]
        public async Task DeletePhotoById()
        {
            PhotoService sut = new PhotoService(_mockContext.Object);

            sut.DeletePhotoById(45);

            int newCount = await sut.Photos.CountAsync();

            Assert.Equal(newCount, 1);
        }
    }
}
