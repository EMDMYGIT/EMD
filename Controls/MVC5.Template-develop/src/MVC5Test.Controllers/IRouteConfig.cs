using System.Web.Routing;

namespace MVC5Test.Controllers
{
    public interface IRouteConfig
    {
        void RegisterRoutes(RouteCollection routes);
    }
}
