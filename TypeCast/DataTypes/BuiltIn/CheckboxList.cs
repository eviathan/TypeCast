using TypeCast;
using System;
using TypeCast.Core;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using TypeCast.Attributes;
using TypeCast.ContentTypes;
using Umbraco.Web;
using TypeCast.Extensions;
using System.Drawing;
using System.Collections.ObjectModel;

namespace TypeCast.DataTypes.BuiltIn
{
    /// <summary>
    /// Represents Umbraco's built-in checkbox list data type
    /// </summary>
    [DataType(name: BuiltInDataTypes.CheckboxList, propertyEditorAlias: BuiltInPropertyEditorAliases.CheckBoxList)]
    [DoNotSyncDataType][BuiltInDataType]
    public class CheckboxList : MultiselectDataType
    {
		public static implicit operator CheckboxList(string value)
		{
			var result = new CheckboxList();
			result.Initialise(value);
			return result;
		}

		public static implicit operator CheckboxList(string[] values)
		{
			var result = new CheckboxList();
			result.Initialise(string.Join(",", values));
			return result;
		}
	}
}