using System;

namespace TypeCast.Attributes
{
    internal abstract class BuiltInTypeAttribute : Attribute { public abstract string BuiltInTypeName { get; } }
}