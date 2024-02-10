using System;

namespace MVC5Test.Components.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllowUnauthorizedAttribute : Attribute
    {
    }
}
