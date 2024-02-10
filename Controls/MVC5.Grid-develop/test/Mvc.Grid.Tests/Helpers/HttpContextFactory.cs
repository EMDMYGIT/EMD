using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NonFactors.Mvc.Grid.Tests
{
    public class HttpContextFactory
    {
        public static HttpContextBase CreateHttpContextBase(String query)
        {
            HttpRequest request = new HttpRequest(String.Empty, "http://localhost:4601/", query);
            HttpResponse response = new HttpResponse(new StringWriter());
            HttpContext context = new HttpContext(request, response);

            RouteValueDictionary route = request.RequestContext.RouteData.Values;
            route["controller"] = "Home";
            route["action"] = "Index";

            RouteTable.Routes.Clear();
            RouteTable.Routes.MapRoute(
                "Default",
                "{controller}/{action}");

            return new HttpContextWrapper(context);
        }
    }
}
