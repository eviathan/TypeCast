
using TypeCast;
using TypeCast.ContentTypes;
using TypeCast.DataTypes;
using TypeCast.DataTypes.BuiltIn;
using TypeCast.Attributes;
using TypeCast.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System;
using TypeCast.Core;

namespace TypeCast.DataTypes.BuiltIn
{
    [DataType(BuiltInPropertyEditorAliases.Integer, BuiltInDataTypes.Numeric)]
    [DoNotSyncDataType][BuiltInDataType]
    public class Numeric : IUmbracoIntegerDataType
    {
        public int Value { get; set; }

		public static implicit operator Numeric(int value)
		{
			return new Numeric() { Value = value };
		}

		/// <summary>
		/// Initialises the instance from the db value
		/// </summary>
		public void Initialise(int dbValue)
        {
            Value = dbValue;
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public int Serialise()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}