using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeCast.Extensions;
using TypeCast.Exceptions;
using TypeCast.Attributes.ContentTypes;

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

        private string PartialViewPath { get; set; }

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
                DocumentName = docAttr.Name;
            }
            _typeName = decoratedType.FullName;
            Initialised = true;
        }

        public string GetRelativePartialViewPath()
        {
            //TODO: Make path configurable with default value
            if (PartialViewPath != null)
            {
                return $"{PartialViewPath}{DocumentName}.cshtml";
            }
            else
            {
                string nestedContentPath = IsNestedContent ? "NestedContent/" : string.Empty;
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
                //TODO: Make path configurable with default value
                string nestedContentPath = IsNestedContent ? "Partials/NestedContent/" : string.Empty;
                return $"~/Views/{nestedContentPath}{DocumentName.ToPascalCase()}.cshtml";
            }
        }

        public bool Initialised { get; private set; }
    }
}
