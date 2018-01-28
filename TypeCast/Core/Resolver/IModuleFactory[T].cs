using System;
using System.Collections.Generic;

namespace TypeCast.Core.Resolver
{
    public interface IModuleFactory<Tinterface> : IModuleFactory where Tinterface : ICodeFirstEntityModule
    {
        new Tinterface CreateInstance(IEnumerable<ICodeFirstEntityModule> dependencies);
    }
}