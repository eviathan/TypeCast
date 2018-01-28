using System;
using System.Collections.Generic;
using System.Reflection;

namespace TypeCast.Core.Resolver
{
    public interface ICodeFirstEntityModule
    {
        void Initialise(IEnumerable<Type> classes);
    }
}