﻿using TypeCast;
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
    /// Represents Umbraco's built-in multi-select dropdown data type
    /// </summary>
    [DataType(name: BuiltInDataTypes.DropdownMultiple, propertyEditorAlias: BuiltInPropertyEditorAliases.DropDownMultiple)]
    [DoNotSyncDataType][BuiltInDataType]
    public class DropdownMultiple : MultiselectDataType
    {
		public static implicit operator DropdownMultiple(string value)
		{
			var result = new DropdownMultiple();
			result.Initialise(value);
			return result;
		}

		public static implicit operator DropdownMultiple(string[] values)
		{
			var result = new DropdownMultiple();
			result.Initialise(string.Join(",", values));
			return result;
		}
	}
}