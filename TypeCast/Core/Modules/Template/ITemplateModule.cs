using TypeCast.Core.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeCast.Core.Modules
{
    public interface ITemplateModule : ICodeFirstEntityModule
    {
        void RegisterTemplates(Type docType);
    }
}
