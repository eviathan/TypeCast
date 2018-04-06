using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeCast.Attributes;
using TypeCast.Core.Resolver;
using Umbraco.Core;

namespace TypeCast.Core.Modules.PartialViewModule
{
    public class PartialViewModuleFactory : ModuleFactoryBase<ICodeFirstEntityModule, IDocumentTypeModule>
    {
        public override ICodeFirstEntityModule CreateInstance(IDocumentTypeModule documentTypeModule)
        {
            return new PartialViewModule(documentTypeModule, ApplicationContext.Current.Services.FileService, ApplicationContext.Current.Services.ContentTypeService);
        }

        public override IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return new Type[] { typeof(PartialViewAttribute) };
        }
    }
}
