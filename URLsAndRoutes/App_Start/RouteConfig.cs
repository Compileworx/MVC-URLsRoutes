using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;
using URLsAndRoutes.Infrastructure;

namespace URLsAndRoutes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //Matches http://localhost/Home
            //Matches http://Localhost/Home/Index
            routes.MapRoute("MyRoute", "{controller}/{action}", new { action = "Index" });

            //Matches http://localhost/Home
            //Matches http://Localhost/Home/Index
            //Matches http://localhost
            routes.MapRoute("MyRoute", "{controller}/{action}", new { action = "Index", controller = "Home" });

            //Supporting a static route
            //http://mydomain.com/Public/Home/Index
            routes.MapRoute("", "Public/{controller}/{action}", new { controller = "Home", action = "Index" });

            //Hard method
            //Route myRoute = new Route("{controller/action}", new MvcRouteHandler());
            //routes.Add("myRoute", myRoute);
            
            //Simple method
            routes.MapRoute("My Route", "{controller}/{action}");

            //Adding a mixed route
            routes.MapRoute("", "x{controller}/{action}");

            //addind a route to resolve to another controller
            //http://localhost/Shop/Index
            //Will resolve to the controller Home/Index
            routes.MapRoute("","Shop/{action}", new {controller = "Home"});

            //addind a route to resolve to another controller and action
            //http://localhost/Shop/Index
            //Will resolve to the controller Home/Index
            routes.MapRoute("", "Shop2/OldAction", new { controller = "Home", action = "Index" });

            //setting up a route with a default parameter
            routes.MapRoute("", "{controller}/{action}/{id}", new { controller = "Home", action = "Index",  id = "defaultValue" });

            //setting up a route with an optional id parameter
            routes.MapRoute("", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            //Setting up a route that will catch all additional parameters
            routes.MapRoute("", "{controller}/{action}/{id}/{*catchall}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            //Setting up a route that check for an extended home controller
            routes.MapRoute("", "{controller}/{action}/{id}/{*catchall}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }, new []{"URLsAndRoutes.AdditionalControllers"});

            //Setting up a route that using regualr expressions
            routes.MapRoute("", "{controller}/{action}/{id}/{*catchall}", new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new { controller = "^H.*"}, new[] { "URLsAndRoutes.AdditionalControllers" });

            //Setting up a route that using regualr expressions extended
            routes.MapRoute("", "{controller}/{action}/{id}/{*catchall}", new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new { controller = "^H.*", action = "^Index$|^About$"}, new[] { "URLsAndRoutes.AdditionalControllers" });

            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { controller = "Product", action = "List", category = (string)null, page = @"\d+" }
            );

            //Setting up a route that using regualr expressions extended
            routes.MapRoute("", "{controller}/{action}/{id}/{*catchall}", new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new { controller = "^H.*", action = "^Index$|^About$", httpMethod = new HttpMethodConstraint("GET") }, new[] { "URLsAndRoutes.AdditionalControllers" });

            //Setting up a route that using a range
            routes.MapRoute("", "{controller}/{action}/{id}/{*catchall}", new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new { controller = "^H.*", action = "^Index$|^About$", httpMethod = new HttpMethodConstraint("GET"), id = new RangeRouteConstraint(10,20) }, new[] { "URLsAndRoutes.AdditionalControllers" });

            //Setting a a route with a custom contrain for only chrome browsers
            routes.MapRoute("ChromeRoute", "{controller}/{action}/{id}", new { controller = "Home", action = "Index" }, new { customConstraint = new UserAgentConstraint("Chrome") });

            //adding the ability to use attributes
            //Look in the controller for details
            routes.MapMvcAttributeRoutes();

            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
