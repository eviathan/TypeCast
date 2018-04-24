﻿using System;
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
        private string _typeName;

        private string _NestedContentDefaultPartialViewPath =>
            ConfigurationManager.AppSettings.AllKeys.Contains("CodeFirstNestedContentDefaultPartialViewPath")
                ? ConfigurationManager.AppSettings["CodeFirstNestedContentDefaultPartialViewPath"]
                : "NestedContent/";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">The name of the partial view</param>
        /// <param name="path">Relative path without precedding forward slash i.e. "MyViews/MySubViews/"</param>
        public PartialViewAttribute(string name = null, string path = null)
        {
            DocumentName = name;
            PartialViewPath = path;
        }

        public string DocumentName { get; private set; }

        private string _partialViewPath = string.Empty;
        private string PartialViewPath
        {
            get
            {
                return _partialViewPath;
            }
            set
            {
                _partialViewPath = SanitisePath(value);
            }
        }

        public bool IsNestedContent { get; private set; }

        public string DecoratedTypeFullName
        {
            get
            {
                return _typeName;
            }
            set
            {
                _typeName = value;
            }
        }

        public void Initialise(Type decoratedType)
        {
            var docAttr = decoratedType.GetCodeFirstAttribute<DocumentTypeAttribute>();
            if (docAttr == null)
            {
                throw new AttributeInitialisationException("[Template] can only be applied to classes which also have a [DocumentType] attribute. Affected type: " + decoratedType.FullName);
            }
            if(docAttr is NestedContentAttribute)
            {
                IsNestedContent = true;
            }
            if (DocumentName == null)
            {
                DocumentName = docAttr.Name.ToPascalCase();
            }
            _typeName = decoratedType.FullName;
            Initialised = true;
        }

        public string GetRelativePartialViewPath()
        {
            if (PartialViewPath != null)
            {
                return $"{PartialViewPath}{DocumentName}.cshtml";
            }
            else
            {
                string nestedContentPath = IsNestedContent ? _NestedContentDefaultPartialViewPath : string.Empty;
                return $"{nestedContentPath}{DocumentName.ToPascalCase()}.cshtml";
            }
        }

        public string GetAbsolutePartialViewPath()
        {
            if (PartialViewPath != null)
            {
                return $"~/Views/{PartialViewPath}{DocumentName}.cshtml";
            }
            else
            {
                string nestedContentPath = IsNestedContent ? $"Partials/{_NestedContentDefaultPartialViewPath}" : string.Empty;
                return $"~/Views/{nestedContentPath}{DocumentName.ToPascalCase()}.cshtml";
            }
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
