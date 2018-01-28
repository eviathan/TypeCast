using Umbraco.Core.Models;

namespace TypeCast.Core.Modules
{
    public interface IContentModelModule
    {
        object ConvertToModel(IPublishedContent content, CodeFirstModelContext parentContext = null);
    }
}