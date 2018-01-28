using System;
using System.Reflection;

namespace TypeCast.Attributes
{
    public interface IDataTypeRedirect
    {
        Type Redirect(PropertyInfo property);
        object GetRedirectedValue(object originalDataTypeObject);
        object GetOriginalDataTypeObject(object redirectedValue);
    }
}