using TypeCast.Core.Resolver;
using System;
using Umbraco.Core;

namespace TypeCast.Core.Modules
{
    public class PreValueCacheModuleFactory : ModuleFactoryBase<IPreValueCacheModule, IDataTypeModule>
    {
        public override System.Collections.Generic.IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return new Type[] { };
        }

        public override IPreValueCacheModule CreateInstance(IDataTypeModule dataTypeModule)
        {
            return new PreValueCacheModule(dataTypeModule, ApplicationContext.Current.Services.DataTypeService);
        }
    }
}