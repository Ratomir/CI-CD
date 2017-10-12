using Moq;
using ms17.BLL.Tests.Model;
using ms17.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace ms17.BLL.Tests
{
    public class CommentServiceShould : BaseServiceTest
    {
        public CommentServiceShould()
        {
            List<Comment> lstComment = new List<Comment>()
            {
                new Comment()
                {
                    Body = "Some body text 1",
                    CommentID = 1,
                    Subject = "Text of subject 1",
                    UserName = "ratomirx"
                },
                new Comment()
                {
                    Body = "Some body text 2",
                    CommentID = 2,
                    Subject = "Text of subject 2",
                    UserName = "ratomiry"
                },
                new Comment()
                {
                    Body = "Some body text 3",
                    CommentID = 3,
                    Subject = "Text of subject 3",
                    UserName = "ratomirz"
                }
            };

            Mock<DbSet<Comment>> mockComment = new Mock<DbSet<Comment>>().SetupData(lstComment);

            _mockContext.Setup(p => p.Comments).Returns(mockComment.Object);
        }

        [Fact]
        public async Task GetAllComments()
        {
            CommentService sut = new CommentService(_mockContext.Object);

            int count = await sut.Comments.CountAsync();

            Assert.Equal(count, 3);
        }

        [Fact]
        public void CreateComment()
        {
            Comment oneComment = new Comment()
            {
                Body = "New comment body 1",
                PhotoID = 45,
                Subject = "New comment subject 1",
                UserName = "Super username 1"
            };

            CommentService sut = new CommentService(_mockContext.Object);

            sut.CreateComment(oneComment);

            int count = sut.Comments.Count();

            Assert.Equal(count, 4);
        }

        [Fact]
        public void ThrowExceptionWhenNullWhenDeletePhotoById()
        {
            CommentService sut = new CommentService(_mockContext.Object);

            int photoId;

            Assert.Throws<NullReferenceException>(() => sut.DeleteCommentById(543, out photoId));
        }


    }
}
