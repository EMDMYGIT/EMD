using System.Collections.Generic;
using System.Xml.Linq;

namespace MVC5Test.Components.Mvc
{
    public interface IMvcSiteMapParser
    {
        IEnumerable<MvcSiteMapNode> GetNodeTree(XElement siteMap);
    }
}
