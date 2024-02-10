using MVC5Test.Resources;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC5Test.Components.Mvc
{
    public class DisplayNameMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type container, Func<Object> model, Type type, String property)
        {
            ModelMetadata metadata = base.CreateMetadata(attributes, container, model, type, property);
            if (container != null) metadata.DisplayName = ResourceProvider.GetPropertyTitle(container, property);

            return metadata;
        }
    }
}
