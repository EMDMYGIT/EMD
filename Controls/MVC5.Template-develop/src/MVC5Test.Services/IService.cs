using System;

namespace MVC5Test.Services
{
    public interface IService : IDisposable
    {
        Int32 CurrentAccountId { get; set; }
    }
}
