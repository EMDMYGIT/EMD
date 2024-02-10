using MVC5Test.Components.Alerts;
using System;
using System.Web.Mvc;

namespace MVC5Test.Validators
{
    public interface IValidator : IDisposable
    {
        ModelStateDictionary ModelState { get; set; }
        Int32 CurrentAccountId { get; set; }
        AlertsContainer Alerts { get; set; }
    }
}
