using TypeCast.Attributes;
using TypeCast.Core.Resolver;
using System;

namespace TypeCast.Core.Modules
{
    public class DocumentModelModuleFactory : ModuleFactoryBase<IDocumentModelModule, IDataTypeModule, IDocumentTypeModule>
    {
        public override System.Collections.Generic.IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return new Type[] { typeof(DocumentTypeAttribute) };
        }

        public override IDocumentModelModule CreateInstance(IDataTypeModule dataTypeModule, IDocumentTypeModule documentTypeModule)
        {
            return new DocumentModelModule(dataTypeModule, documentTypeModule);
        }
    }

}