using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypeCast.Extensions;
using TypeCast.Exceptions;

namespace TypeCast.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CodeFirstCommonBaseAttribute : CodeFirstAttribute
    {

    }

}