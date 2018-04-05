﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TypeCast.Attributes;
using TypeCast.ContentTypes;
using TypeCast.DataTypes.BuiltIn;
using TypeCast.Models;
using Umbraco.Web.Models;

namespace System.Web.Mvc.Html
{
    /// <summary>
    /// Document Html Helper Extensions
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Returns the views for each nested content item in the object that is represented by the specified expression.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString NestedContent<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
            where TModel : DocumentTypeBase, new()
            where TProperty : NestedContent
        {
            var member = (MemberExpression)expression.Body;
            var propertyName = member.Member.Name;
            var propertyValue = expression.Compile()(htmlHelper.ViewData.Model);

            // Get atttribute types
            var propertyInfo = (PropertyInfo) member.Member;
            var nestedContentPropertyAttribute = propertyInfo.GetCustomAttributes(typeof(NestedContentPropertyAttribute), true).FirstOrDefault() as NestedContentPropertyAttribute;
            if (nestedContentPropertyAttribute == null) throw new ArgumentException($"NestedContent({typeof(TProperty).Name}) property requires a {nameof(NestedContentPropertyAttribute)} attribute.");
            
            var allowedNestedContentTypes = nestedContentPropertyAttribute.NestedTypes;

            var mvcHtmlStrings = new List<MvcHtmlString>();
            
            foreach (var model in propertyValue.Where(x => allowedNestedContentTypes.Contains(x.Value.GetType())))
            {
                if (model == null) continue;

                var partialViewName = model.NcContentTypeAlias;          
                mvcHtmlStrings.Add(htmlHelper.Partial(partialViewName, model.Value, htmlHelper.ViewData));
            }    

            return Concat(mvcHtmlStrings);
        }

        #region Helper Methods
        private static MvcHtmlString Concat(IEnumerable<MvcHtmlString> items)
        {
            var sb = new StringBuilder();
            foreach (var item in items.Where(i => i != null))
                sb.Append(item.ToHtmlString());
            return MvcHtmlString.Create(sb.ToString());
        }
        #endregion
    }
}