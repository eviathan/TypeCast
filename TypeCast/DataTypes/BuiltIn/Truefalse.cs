
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
    [DataType("Umbraco.TrueFalse", "True/false")]
    [DoNotSyncDataType][BuiltInDataType]
    public class TrueFalse : IUmbracoIntegerDataType
    {

        public bool Value { get; set; }

		public static implicit operator TrueFalse(bool value)
		{
			return new TrueFalse() { Value = value };
		}

		/// <summary>
		/// Initialises the instance from the db value
		/// </summary>
		public void Initialise(int dbValue)
        {
            if (dbValue > 0)
            {
                Value = true;
            }
            else
            {
                Value = false;
            }
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public int Serialise()
        {
            return Value ? 1 : 0;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}