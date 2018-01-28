using TypeCast.Core.Resolver;
using System;
using Umbraco.Core;

namespace TypeCast.Core.Modules
{
    public class PropertyModuleFactory : ModuleFactoryBase<IPropertyModule, IDataTypeModule>
    {
        public override IPropertyModule CreateInstance(IDataTypeModule dataTypeModule)
        {
            return new PropertyModule(dataTypeModule, ApplicationContext.Current.Services.DataTypeService);
        }

        public override System.Collections.Generic.IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return new Type[] { };
        }
    }
}