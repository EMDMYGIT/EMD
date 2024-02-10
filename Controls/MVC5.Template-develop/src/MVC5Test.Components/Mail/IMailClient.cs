using System;
using System.Threading.Tasks;

namespace MVC5Test.Components.Mail
{
    public interface IMailClient
    {
        Task SendAsync(String email, String subject, String body);
    }
}
