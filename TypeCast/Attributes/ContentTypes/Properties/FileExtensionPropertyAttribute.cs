using TypeCast.ContentTypes;
using System;

namespace TypeCast.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FileExtensionPropertyAttribute : ContentPropertyAttribute
    {
        public FileExtensionPropertyAttribute(string name = null, bool mandatory = false, string description = "", int sortOrder = 0, bool addTabAliasToPropertyAlias = true, string dataType = null)
            : base(name, SpecialAliases.FileExtension, mandatory, description, sortOrder, addTabAliasToPropertyAlias, dataType)
        {

        }
    }
}