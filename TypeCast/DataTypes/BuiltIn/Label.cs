
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
    [DataType("Umbraco.NoEdit", "Label")]
    [DoNotSyncDataType][BuiltInDataType]
    public class Label : IUmbracoNvarcharDataType
    {
		public static implicit operator Label(string value)
		{
			return new Label() { Value = value };
		} 

        public string Value { get; set; }

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