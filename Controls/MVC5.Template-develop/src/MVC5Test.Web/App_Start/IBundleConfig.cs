using System.Web.Optimization;

namespace MVC5Test.Web
{
    public interface IBundleConfig
    {
        void RegisterBundles(BundleCollection bundles);
    }
}
