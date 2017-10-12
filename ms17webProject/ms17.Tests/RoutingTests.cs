using System;
using System.Web.Routing;
using System.Web.Mvc;
using Xunit;
using ms17.Web.Tests.Doubles;

namespace ms17.Web.Tests
{
    public class RoutingTests
    {
        [Fact]
        public void Test_Default_Route_ControllerOnly()
        {
            //This test checks the default route when only the controller is specified
            //Arrange
            var context = new FakeHttpContextForRouting(requestUrl: "~/ControllerName");
            var routes = new RouteCollection();
            ms17.RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.NotNull(routeData);
            Assert.Equal("ControllerName", routeData.Values["controller"]);
            Assert.Equal("Index", routeData.Values["action"]);
            Assert.Equal(UrlParameter.Optional, routeData.Values["id"]);
        }

        [Fact]
        public void Test_PhotoRoute_With_PhotoID()
        {
            //This test checks the PhotoRoute route. 
            //Arrange
            var context = new FakeHttpContextForRouting(requestUrl: "~/photo/2");
            var routes = new RouteCollection();
            ms17.RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.NotNull(routeData);
            Assert.Equal("Photo", routeData.Values["controller"]);
            Assert.Equal("Display", routeData.Values["action"]);
            Assert.Equal("2", routeData.Values["id"]);
        }

        [Fact]
        public void Test_PhotoTitleRoute_WithTitle()
        {
            //This test checks the PhotoTitleRoute route a title is specified
            //Arrange
            var context = new FakeHttpContextForRouting(requestUrl: "~/Photo/Title/My%20Photo");
            var routes = new RouteCollection();
            ms17.RouteConfig.RegisterRoutes(routes);

            // Act
            RouteData routeData = routes.GetRouteData(context);

            // Assert
            Assert.NotNull(routeData);
            Assert.Equal("Photo", routeData.Values["controller"]);
            Assert.Equal("DisplayByTitle", routeData.Values["action"]);
            Assert.Equal("My%20Photo", routeData.Values["title"]);
        }

    }
}
