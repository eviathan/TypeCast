using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeCast.ContentTypes;
using TypeCast.DataTypes;
using TypeCast.Extensions;
using TypeCast.Exceptions;
using System.Reflection;
using TypeCast.Core;

namespace TypeCast.Converters
{
    /// <summary>
    /// A converter which can convert between a storage type of Tdb and
    /// a C# type of Tentity
    /// </summary>
    /// <typeparam name="Tdb">The database storage type (int, string or DateTime)</typeparam>
    /// <typeparam name="Tentity">The code-first data type</typeparam>
    public interface IDataTypeConverter<Tdb, Tentity> : IDataTypeConverter
    {
        /// <summary>
        /// Creates an instance of Tentity by deserialising or converting the input
        /// </summary>
        Tentity Create(Tdb input, Action<object> registerContext = null);

        /// <summary>
        /// Creates an instance of Tdb by serialising or converting the input
        /// </summary>
         Tdb Serialise(Tentity input);
    }

    public interface IDataTypeConverter
    {
        object Create(object input, Action<object> registerContext = null);
        object Serialise(object input);
    }
}
