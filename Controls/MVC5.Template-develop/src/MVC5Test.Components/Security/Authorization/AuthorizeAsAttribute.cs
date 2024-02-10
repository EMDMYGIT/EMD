using System;

namespace MVC5Test.Components.Security
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizeAsAttribute : Attribute
    {
        public String Action { get; }
        public String Area { get; set; }
        public String Controller { get; set; }

        public AuthorizeAsAttribute(String action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            Action = action;
        }
    }
}
