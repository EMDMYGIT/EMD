﻿using MVC5Test.Components.Alerts;
using MVC5Test.Components.Mvc;
using MVC5Test.Data.Core;
using MVC5Test.Objects;
using MVC5Test.Resources;
using MVC5Test.Resources.Form;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MVC5Test.Validators
{
    public abstract class BaseValidator : IValidator
    {
        public ModelStateDictionary ModelState { get; set; }
        public Int32 CurrentAccountId { get; set; }
        public AlertsContainer Alerts { get; set; }
        protected IUnitOfWork UnitOfWork { get; }

        protected BaseValidator(IUnitOfWork unitOfWork)
        {
            ModelState = new ModelStateDictionary();
            Alerts = new AlertsContainer();
            UnitOfWork = unitOfWork;
        }

        protected Boolean IsSpecified<TView>(TView view, Expression<Func<TView, Object>> property) where TView : BaseView
        {
            Boolean isSpecified = property.Compile().Invoke(view) != null;

            if (!isSpecified)
            {
                UnaryExpression unary = property.Body as UnaryExpression;
                if (unary == null)
                    ModelState.AddModelError(property, Validations.Required, ResourceProvider.GetPropertyTitle(property));
                else
                    ModelState.AddModelError(property, Validations.Required, ResourceProvider.GetPropertyTitle(unary.Operand));
            }

            return isSpecified;
        }

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
