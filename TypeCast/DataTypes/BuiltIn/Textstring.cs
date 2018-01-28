
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
    [DataType("Umbraco.Textbox", "Textstring")]
    [DoNotSyncDataType][BuiltInDataType]
    public class Textstring : IUmbracoNvarcharDataType
    {
        public string Value { get; set; }

		public static implicit operator Textstring(string value)
		{
			return new Textstring() { Value = value };
		}

		/// <summary>
		/// Initialises the instance from the db value
		/// </summary>
		public void Initialise(string dbValue)
        {
            Value = dbValue;
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public string Serialise()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}