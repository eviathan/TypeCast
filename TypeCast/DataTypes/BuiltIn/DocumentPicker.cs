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
using TypeCast.Exceptions;
using TypeCast.Core.Modules;
using TypeCast.DataTypes.BuiltIn;

namespace TypeCast.DataTypes.BuiltIn
{
	/// <summary>
	/// A strongly-typed content picker which provides a strongly-typed model of the picked document.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[DataType(propertyEditorAlias: BuiltInPropertyEditorAliases.MultiNodeTreePicker)]
	public class DocumentPicker<T> : NodePicker<T, DocumentNodeDetails>, IDocumentPicker, IPreValueFactory where T : DocumentTypeBase, new()
	{
		public static implicit operator DocumentPicker<T>(T[] values)
		{
			var val = new BuiltIn.DocumentPicker<T>();
			val.SetCollection(values);
			return val;
		}

        public DocumentPicker() : base(NodeType.content) { }

        public override string ToHtmlString()
        {
            if (Items.Count == 1)
            {
                var item = Items.First();
                if (item is IHtmlString)
                {
                    return (item as IHtmlString).ToHtmlString();
                }
                else
                {
                    return HttpUtility.HtmlEncode(item.ToString());
                }
            }
            else if (Items.Count > 1)
            {
                return Items.Count + " document items selected";
            }
            else
            {
                return "No document items selected";
            }
        }

        public override string ToString()
        {
            if (Items.Count == 1)
            {
                var item = Items.First();
                return item.ToString();
            }
            else if (Items.Count > 1)
            {
                return Items.Count + " document items selected";
            }
            else
            {
                return "No document items selected";
            }
        }

        protected override T GetModelFromId(int id)
        {
            var node = new Umbraco.Web.UmbracoHelper(Umbraco.Web.UmbracoContext.Current).TypedContent(id);
            return node == null ? null : node.ConvertDocumentToModel<T>(CodeFirstModelContext.GetContext(this));
        }
    }

    internal interface IDocumentPicker { }
}
