using System.Web.Mvc;
using ms17.Controllers;
using Xunit;

namespace ms17.Web.Tests.Controllers
{
    public class HomeControllerShould
    {
        private readonly HomeController _sut;
        public HomeControllerShould()
        {
            _sut = new HomeController();
        }
        
        [Fact]
        public void ReturnViewForIndex()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.Equal("Your application description page.", result.ViewBag.Message);
        }

        [Fact]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}
