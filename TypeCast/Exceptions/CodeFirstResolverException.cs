using System;

namespace TypeCast.Exceptions
{
    [Serializable]
    public class CodeFirstResolverException : Exception
    {
        public CodeFirstResolverException(string message) : base(message) { }
    }
}