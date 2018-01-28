using TypeCast.Attributes;
using System;
using TypeCast.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeCast.DataTypes.BuiltIn
{
    /// <summary>
    /// Represents Umbraco's built-in dropdown list data type
    /// </summary>
    [DataType(name: BuiltInDataTypes.Dropdown, propertyEditorAlias: BuiltInPropertyEditorAliases.DropDown)]
    [DoNotSyncDataType][BuiltInDataType]
    public class DropdownList : SingleSelectDataType, IUmbracoNtextDataType
    {
		public static implicit operator DropdownList(string value)
		{
			return new DropdownList() { SelectedValue = value };
		}

		public void Initialise(string dbValue)
        {
            base.Initialise(dbValue);
        }
    }
}
