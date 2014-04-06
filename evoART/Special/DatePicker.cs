using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace evoART.Special
{
    public static class DatePicker
    {
        public static MvcHtmlString DatePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            var propertyName = ExpressionHelper.GetExpressionText(expression);
            var controlGroupWrapper = new TagBuilder("div");
            controlGroupWrapper.AddCssClass("control-group");

            controlGroupWrapper.InnerHtml += htmlHelper.LabelFor(expression, new Dictionary<string, object> { { "class", "control-label" } }).ToHtmlString();

            var controlWrapper = new TagBuilder("div");
            controlWrapper.AddCssClass("controls");

            var datePicker = new TagBuilder("div");
            datePicker.AddCssClass("input-append date datepicker");
            datePicker.MergeAttribute("data-date-format", "dd/mm/yyy");

            var spanWrapper = new TagBuilder("span");
            spanWrapper.AddCssClass("add-on");

            var iconThWrapper = new TagBuilder("i");
            iconThWrapper.AddCssClass("icon-th");

            //GetPropValue(htmlHelper.ViewData.Model, propertyName);
            var inputTag = htmlHelper.EditorFor(expression).ToHtmlString();


            spanWrapper.InnerHtml += iconThWrapper.ToString(TagRenderMode.Normal);


            datePicker.ToString(TagRenderMode.Normal);
            datePicker.InnerHtml += inputTag + spanWrapper.ToString(TagRenderMode.Normal); ;

            controlWrapper.InnerHtml += datePicker.ToString(TagRenderMode.Normal);

            controlGroupWrapper.InnerHtml += controlWrapper.ToString(TagRenderMode.Normal);



            return MvcHtmlString.Create(controlGroupWrapper.ToString(TagRenderMode.Normal));

        }
    }
}