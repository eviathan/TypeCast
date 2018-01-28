using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeCast.Core.Resolver
{
    public interface IClassFileGenerator
    {
        void GenerateClassFiles(string nameSpace, string folderPath);
    }
}
