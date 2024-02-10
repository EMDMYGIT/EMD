using MVC5Test.Components.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC5Test.Tests.Unit.Components.Mvc
{
    public class DisplayNameMetadataProviderProxy : DisplayNameMetadataProvider
    {
        public ModelMetadata BaseCreateMetadata(IEnumerable<Attribute> attributes, Type container, Func<Object> model, Type type, String property)
        {
            return CreateMetadata(attributes, container, model, type, property);
        }
    }
}
