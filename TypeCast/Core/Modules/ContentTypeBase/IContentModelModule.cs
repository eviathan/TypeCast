using Umbraco.Core.Models;

namespace TypeCast.Core.Modules
{
    public interface IContentModelModule
    {
        object ConvertToContentModel(IContent content, CodeFirstModelContext parentContext = null);

        object ConvertToModel(IPublishedContent content, CodeFirstModelContext parentContext = null);
    }
}