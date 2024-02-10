using MVC5Test.Resources.Form;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVC5Test.Components.Mvc
{
    public class RequiredAdapter : RequiredAttributeAdapter
    {
        public RequiredAdapter(ModelMetadata metadata, ControllerContext context, RequiredAttribute attribute)
            : base(metadata, context, attribute)
        {
            Attribute.ErrorMessage = Validations.Required;
        }
    }
}
