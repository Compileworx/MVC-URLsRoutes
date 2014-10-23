using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using System.Web.Routing;
using System.Reflection;

namespace URLsAndRoutes.Tests
{
    [TestClass]
    public class URLTests
    {
        private HttpContextBase createHttpContent(string targetUrl = null, string httpMethod = "GET")
        {
            Mock<HttpRequestBase> mock = new Mock<HttpRequestBase>();
            mock.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mock.Setup(m => m.HttpMethod).Returns(httpMethod);

            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mock.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            return mockContext.Object;
        }

        private void TestRouteMatch(string URL, string controller, string action, object routeProperties = null, string httpMethod = "GET")
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            RouteData result = routes.GetRouteData(createHttpContent(URL, httpMethod));

            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncommingRouteResult(result, controller, action, routeProperties));
        }

        private bool TestIncommingRouteResult(RouteData routeResult, string controller, string action, object routeProperties)
        {
            Func<object, object, bool> valCompare = (v1, v2) =>
            {
                return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
            };

            bool result = valCompare(routeResult.Values["action"], action) && valCompare(routeResult.Values["controller"],controller);

            if (routeProperties != null)
            {
                PropertyInfo[] propInfo = routeProperties.GetType().GetProperties();
                foreach (PropertyInfo p in propInfo)
                {
                    if (!(routeResult.Values.ContainsKey(p.Name) && valCompare(routeResult.Values[p.Name], p.GetValue(routeProperties))))
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

    }
}
