using TypeCast.Attributes;
using TypeCast.Core.Resolver;
using TypeCast.Extensions;
using System;
using System.Collections.Generic;
using Umbraco.Core;

namespace TypeCast.Core.Modules
{
    public class TemplateModuleFactory : ModuleFactoryBase<ITemplateModule, IDocumentTypeModule>
    {
        public override IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return new Type[] { typeof(TemplateAttribute) };
        }

        public override ITemplateModule CreateInstance(IDocumentTypeModule documentTypeModule)
        {
            return new TemplateModule(documentTypeModule, ApplicationContext.Current.Services.FileService, ApplicationContext.Current.Services.ContentTypeService);
        }
    }
}