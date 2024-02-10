using System;

namespace MVC5Test.Components.Logging
{
    public interface ILogger
    {
        void Log(String message);
        void Log(Exception exception);
    }
}
