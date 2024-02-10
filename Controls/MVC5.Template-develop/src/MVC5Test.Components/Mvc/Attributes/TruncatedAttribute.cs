using System;

namespace MVC5Test.Components.Mvc
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class TruncatedAttribute : Attribute
    {
    }
}
