using System;

namespace TypeCast.Core.Modules
{
    public interface IContentTypeModuleBase
    {
        bool TryGetContentType(string alias, out ContentTypeRegistration registration);
        bool TryGetContentType(Type type, out ContentTypeRegistration registration);
    }
}