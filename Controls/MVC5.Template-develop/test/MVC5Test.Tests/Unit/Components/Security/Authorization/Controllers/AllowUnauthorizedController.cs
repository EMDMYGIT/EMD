using MVC5Test.Components.Security;
using System.Diagnostics.CodeAnalysis;

namespace MVC5Test.Tests.Unit.Components.Security
{
    [AllowUnauthorized]
    [ExcludeFromCodeCoverage]
    public class AllowUnauthorizedController : AuthorizedController
    {
    }
}
