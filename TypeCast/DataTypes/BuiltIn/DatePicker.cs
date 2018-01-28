
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
    [DataType("Umbraco.Date", "Date Picker")]
    [DoNotSyncDataType][BuiltInDataType]
    public class DatePicker : IUmbracoDateDataType
    {
        public DateTime Value { get; set; }

		public static implicit operator DatePicker(DateTime dateTime)
		{
			return new DatePicker() { Value = dateTime };
		}

		/// <summary>
		/// Initialises the instance from the db value
		/// </summary>
		public void Initialise(DateTime dbValue)
        {
            Value = dbValue;
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public DateTime Serialise()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}