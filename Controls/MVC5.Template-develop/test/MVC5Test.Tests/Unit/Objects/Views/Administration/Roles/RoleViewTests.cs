using MVC5Test.Components.Extensions;
using MVC5Test.Objects;
using Xunit;

namespace MVC5Test.Tests.Unit.Objects
{
    public class RoleViewTests
    {
        #region RoleView()

        [Fact]
        public void RoleView_CreatesEmpty()
        {
            JsTree actual = new RoleView().Permissions;

            Assert.Empty(actual.SelectedIds);
            Assert.Empty(actual.Nodes);
        }

        #endregion
    }
}
