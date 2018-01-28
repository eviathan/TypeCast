
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
    [DataType("Umbraco.DateTime", "Date Picker with time")]
    [DoNotSyncDataType][BuiltInDataType]
    public class DatePickerWithTime : DatePicker
    {
		public static implicit operator DatePickerWithTime(DateTime dateTime)
		{
			return new DatePickerWithTime() { Value = dateTime };
		}

	}
}