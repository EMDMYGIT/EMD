using MVC5Test.Data.Core;
using MVC5Test.Objects;
using MVC5Test.Tests.Objects;
using System.Data.Entity;

namespace MVC5Test.Tests.Data
{
    public class TestingContext : Context
    {
        #region Test

        protected DbSet<TestModel> TestModels { get; set; }

        #endregion

        public void DropData()
        {
            Set<RolePermission>().RemoveRange(Set<RolePermission>());
            Set<Permission>().RemoveRange(Set<Permission>());
            Set<Account>().RemoveRange(Set<Account>());
            Set<Role>().RemoveRange(Set<Role>());

            SaveChanges();
        }
    }
}
