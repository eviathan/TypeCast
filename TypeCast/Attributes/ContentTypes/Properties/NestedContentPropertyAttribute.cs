using TypeCast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TypeCast.Extensions;
using TypeCast.Exceptions;
using TypeCast.ContentTypes;
using Umbraco.Core.Models;
using Umbraco.Core;
using TypeCast.DataTypes.BuiltIn;

namespace TypeCast.Attributes
{
    /// <summary>
    /// Specifies that a property should be used as a nested document property on a document type.
    /// Any properties which are not set will be inferred from the property metadata and the
    /// data type metadata if possible.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NestedContentPropertyAttribute : ContentPropertyAttribute, IInitialisablePropertyAttribute
    {
        /// <summary>
        /// The types of document allowed to be nested in this property.
        /// </summary>
        public Type[] NestedTypes { get; set; }

        /// <summary>
        /// Specifies that a property should be used as a document property on a document type.
        /// Any properties which are not set will be inferred from the property metadata and the
        /// data type metadata if possible.
        /// </summary>
        /// <param name="name">Friendly name of the property</param>
        /// <param name="alias">Alias of the property</param>
        /// <param name="mandatory">if set to <c>true</c> [mandatory].</param>
        /// <param name="description">The description.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="addTabAliasToPropertyAlias">if set to <c>true</c> add's the tab's alias as a suffix to the property alias.</param>
        /// <param name="dataType">
        /// <para>The name of the data type to use for this property.</para> 
        /// <para>This property is ignored if the property type is a code-first data type. It should be used
        /// when the property type matches the output type of the relevant property editor value converter.
        /// The specified data type must already exist in Umbraco, it will not be created or updated when
        /// specified using this value.</para>
        /// </param>
        /// <param name="nestedTypes"></param>
        public NestedContentPropertyAttribute(string name = null, string alias = null, bool mandatory = false, string description = "", int sortOrder = 0, bool addTabAliasToPropertyAlias = true, string dataType = null, Type[] nestedTypes = null)
        {
            Name = name;
            Alias = alias;
            Mandatory = mandatory;
            Description = description;
            SortOrder = sortOrder;
            AddTabAliasToPropertyAlias = addTabAliasToPropertyAlias;
            DataType = dataType;
            NestedTypes = nestedTypes;
        }
    }
}