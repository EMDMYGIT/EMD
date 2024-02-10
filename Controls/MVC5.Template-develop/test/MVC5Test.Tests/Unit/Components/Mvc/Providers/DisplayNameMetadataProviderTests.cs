using MVC5Test.Objects;
using MVC5Test.Resources;
using System;
using Xunit;

namespace MVC5Test.Tests.Unit.Components.Mvc
{
    public class DisplayNameMetadataProviderTests
    {
        #region CreateMetadata(IEnumerable<Attribute> attributes, Type container, Func<Object> model, Type type, String property)

        [Fact]
        public void CreateMetadata_SetsDisplayName()
        {
            DisplayNameMetadataProviderProxy provider = new DisplayNameMetadataProviderProxy();

            String actual = provider.BaseCreateMetadata(new Attribute[0], typeof(RoleView), null, typeof(String), "Title").DisplayName;
            String expected = ResourceProvider.GetPropertyTitle(typeof(RoleView), "Title");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateMetadata_NullContainer_DoesNotSetDisplayName()
        {
            DisplayNameMetadataProviderProxy provider = new DisplayNameMetadataProviderProxy();

            String actual = provider.BaseCreateMetadata(new Attribute[0], null, null, typeof(String), "Name").DisplayName;

            Assert.Null(actual);
        }

        #endregion
    }
}
