﻿using MVC5Test.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace MVC5Test.Components.Extensions
{
    public static class BootstrapExtensions
    {
        public static MvcHtmlString FormLabelFor<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, Boolean>> expression, Boolean? required = null)
        {
            return html.FormLabelFor<TModel, Boolean>(expression, required == true);
        }
        public static MvcHtmlString FormLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, Boolean? required = null)
        {
            TagBuilder requiredSpan = new TagBuilder("span");
            TagBuilder label = new TagBuilder("label");
            requiredSpan.AddCssClass("require");

            if (required == true)
                requiredSpan.InnerHtml = "*";

            if (required == null && ModelMetadata.FromLambdaExpression(expression, html.ViewData).IsRequired)
                requiredSpan.InnerHtml = "*";

            label.Attributes["for"] = TagBuilder.CreateSanitizedId(ExpressionHelper.GetExpressionText(expression));
            label.InnerHtml = ResourceProvider.GetPropertyTitle(expression) + requiredSpan;

            return new MvcHtmlString(label.ToString());
        }

        public static MvcHtmlString FormTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.FormTextBoxFor(expression, null, null);
        }
        public static MvcHtmlString FormTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, String format)
        {
            return html.FormTextBoxFor(expression, format, null);
        }
        public static MvcHtmlString FormTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, Object htmlAttributes)
        {
            return html.FormTextBoxFor(expression, null, htmlAttributes);
        }
        public static MvcHtmlString FormTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, String format, Object htmlAttributes)
        {
            RouteValueDictionary attributes = FormHtmlAttributes(expression, htmlAttributes, "form-control");

            return html.TextBoxFor(expression, format, attributes);
        }

        public static MvcHtmlString FormPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            return html.PasswordFor(expression, new { @class = "form-control", autocomplete = "off" });
        }

        public static MvcHtmlString FormTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.FormTextAreaFor(expression, null);
        }
        public static MvcHtmlString FormTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, Object htmlAttributes)
        {
            RouteValueDictionary attributes = FormHtmlAttributes(expression, htmlAttributes, "form-control");
            if (!attributes.ContainsKey("rows")) attributes["rows"] = 6;

            return html.TextAreaFor(expression, attributes);
        }

        public static MvcHtmlString FormDatePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.FormDatePickerFor(expression, null);
        }
        public static MvcHtmlString FormDatePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, Object htmlAttributes)
        {
            RouteValueDictionary attributes = FormHtmlAttributes(expression, htmlAttributes, "form-control datepicker");

            return html.TextBoxFor(expression, "{0:d}", attributes);
        }

        public static MvcHtmlString FormDateTimePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return html.FormDateTimePickerFor(expression, null);
        }
        public static MvcHtmlString FormDateTimePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, Object htmlAttributes)
        {
            RouteValueDictionary attributes = FormHtmlAttributes(expression, htmlAttributes, "form-control datetimepicker");

            return html.TextBoxFor(expression, "{0:g}", attributes);
        }

        private static RouteValueDictionary FormHtmlAttributes(LambdaExpression expression, Object attributes, String cssClass)
        {
            RouteValueDictionary htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes);
            if (!htmlAttributes.ContainsKey("autocomplete")) htmlAttributes["autocomplete"] = "off";
            htmlAttributes["class"] = (cssClass + " " + htmlAttributes["class"]).Trim();
            if (htmlAttributes.ContainsKey("readonly")) return htmlAttributes;

            MemberExpression member = expression.Body as MemberExpression;
            if (member?.Member.IsDefined(typeof(EditableAttribute), false) == true)
            {
                EditableAttribute editable = member.Member.GetCustomAttribute<EditableAttribute>(false);
                if (!editable.AllowEdit) htmlAttributes["readonly"] = "readonly";
            }

            return htmlAttributes;
        }
    }
}
