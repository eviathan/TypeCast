using TypeCast.Attributes;
using TypeCast.Core.Resolver;
using System;
using Umbraco.Core;

namespace TypeCast.Core.Modules
{
    public class DocumentTypeModuleFactory : ModuleFactoryBase<IDocumentTypeModule, IPropertyModule>
    {
        public override System.Collections.Generic.IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return new Type[] { typeof(DocumentTypeAttribute) };
        }

        public override IDocumentTypeModule CreateInstance(IPropertyModule propertyModule)
        {
            return new DocumentTypeModule(propertyModule, ApplicationContext.Current.Services.ContentTypeService);
        }
    }
}