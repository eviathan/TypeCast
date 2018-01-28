using TypeCast.ContentTypes;
using System;

namespace TypeCast.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FileUploadPropertyAttribute : ContentPropertyAttribute
    {
        public FileUploadPropertyAttribute(string name = null, bool mandatory = false, string description = "", int sortOrder = 0, bool addTabAliasToPropertyAlias = true, string dataType = null)
            : base(name, SpecialAliases.FileUpload, mandatory, description, sortOrder, addTabAliasToPropertyAlias, dataType)
        {

        }
    }
}