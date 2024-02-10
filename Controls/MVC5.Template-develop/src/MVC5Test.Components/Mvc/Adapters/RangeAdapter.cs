using MVC5Test.Resources.Form;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVC5Test.Components.Mvc
{
    public class RangeAdapter : RangeAttributeAdapter
    {
        public RangeAdapter(ModelMetadata metadata, ControllerContext context, RangeAttribute attribute)
            : base(metadata, context, attribute)
        {
            Attribute.ErrorMessage = Validations.Range;
        }
    }
}
