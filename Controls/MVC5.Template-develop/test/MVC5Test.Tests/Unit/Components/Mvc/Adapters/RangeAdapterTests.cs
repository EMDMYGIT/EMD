using MVC5Test.Components.Mvc;
using MVC5Test.Resources.Form;
using MVC5Test.Tests.Objects;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Xunit;

namespace MVC5Test.Tests.Unit.Components.Mvc
{
    public class RangeAdapterTests
    {
        #region RangeAdapter(ModelMetadata metadata, ControllerContext context, RangeAttribute attribute)

        [Fact]
        public void RangeAdapter_SetsErrorMessage()
        {
            RangeAttribute attribute = new RangeAttribute(0, 128);
            ModelMetadata metadata = new DataAnnotationsModelMetadataProvider()
                .GetMetadataForProperty(null, typeof(AdaptersModel), "Range");

            new RangeAdapter(metadata, new ControllerContext(), attribute);

            String actual = attribute.ErrorMessage;
            String expected = Validations.Range;

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
