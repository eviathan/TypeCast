using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeCast.Extensions;
using TypeCast.Exceptions;
using TypeCast.Attributes.ContentTypes;
using System.Configuration;
using System.Text.RegularExpressions;

namespace TypeCast.Attributes
{
    /// <summary>
    /// TODO:
    /// - Wire up optional path
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class PartialViewAttribute : MultipleCodeFirstAttribute, IInitialisableAttribute
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">The name of the partial view</param>
        public PartialViewAttribute(string name = null)
        {
            DocumentName = name;
        }

        public string DocumentName { get; private set; }

        public string DecoratedTypeName { get; private set; }

        public string DecoratedTypeFullName { get; private set; }

        public void Initialise(Type decoratedType)
        {
            var docAttr = decoratedType.GetCodeFirstAttribute<DocumentTypeAttribute>();
            if (docAttr == null)
            { 
                throw new AttributeInitialisationException("[PartialView] can only be applied to classes which also have a [DocumentType] attribute. Affected type: " + decoratedType.FullName);
            }

            DecoratedTypeName = decoratedType.Name;
            DecoratedTypeFullName = decoratedType.FullName;
            DocumentName = string.IsNullOrWhiteSpace(docAttr.Name) ? DecoratedTypeName : docAttr.Name;
            Initialised = true;
        } 

        public string GetRelativePartialViewPath()
        {
            return $"{DocumentName}.cshtml";
        }

        public string GetAbsolutePartialViewPath()
        {
            return $"~/Views/Partials/{DocumentName}.cshtml";
        }

        private string SanitisePath(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                // strip trailing and prefixed forward slashes and then readd the trailing
                // so it meets the correct relative path format for umbracos partial constructor
                value = Regex.Replace(value, @"^\/*", string.Empty);
                value = Regex.Replace(value, @"\/*$", string.Empty);
                value += "/";
            }

            return value;
        }

        public bool Initialised { get; private set; }
    }
}
