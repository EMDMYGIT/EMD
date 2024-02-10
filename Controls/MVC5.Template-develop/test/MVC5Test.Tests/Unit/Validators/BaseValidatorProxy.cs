using MVC5Test.Data.Core;
using MVC5Test.Objects;
using MVC5Test.Validators;
using System;
using System.Linq.Expressions;

namespace MVC5Test.Tests.Unit.Validators
{
    public class BaseValidatorProxy : BaseValidator
    {
        public BaseValidatorProxy(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Boolean BaseIsSpecified<TView>(TView view, Expression<Func<TView, Object>> property) where TView : BaseView
        {
            return IsSpecified(view, property);
        }
    }
}
