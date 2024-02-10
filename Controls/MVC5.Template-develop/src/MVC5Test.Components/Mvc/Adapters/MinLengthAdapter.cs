using MVC5Test.Resources.Form;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVC5Test.Components.Mvc
{
    public class MinLengthAdapter : MinLengthAttributeAdapter
    {
        public MinLengthAdapter(ModelMetadata metadata, ControllerContext context, MinLengthAttribute attribute)
            : base(metadata, context, attribute)
        {
            Attribute.ErrorMessage = Validations.MinLength;
        }
    }
}
