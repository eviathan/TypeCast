using TypeCast.Attributes;
using TypeCast.ContentTypes;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;

namespace TypeCast.DataTypes.BuiltIn
{
    [DataType(propertyEditorAlias: BuiltInPropertyEditorAliases.MultiNodeTreePicker)]
    public class SingleDocumentPicker<T> : DocumentPicker<T>, IPickedItem<T>, IHtmlString where T : DocumentTypeBase, new()
    {
		public static implicit operator SingleDocumentPicker<T>(T value)
		{
			var val = new BuiltIn.SingleDocumentPicker<T>();
			val.SetCollection(new T[] { value });
			return val;
		}

		public string Url
        {
            get
            {
                var item = this.FirstOrDefault();
                return item == null || item.NodeDetails == null ? string.Empty : item.NodeDetails.Url;
            }
        }

        public string Name
        {
            get
            {
                var item = this.FirstOrDefault();
                return item == null || item.NodeDetails == null ? string.Empty : item.NodeDetails.Name;
            }
        }

        public T PickedItem
        {
            get
            {
                var item = this.FirstOrDefault();
                return item;
            }
        }

        public override string ToHtmlString()
        {
            var item = Items.FirstOrDefault();
            if (item == null)
            {
                return string.Empty;
            }
            else if (item is IHtmlString)
            {
                return (item as IHtmlString).ToHtmlString();
            }
            else
            {
                return HttpUtility.HtmlEncode(item.ToString());
            }
        }

        public override string ToString()
        {
            var item = Items.FirstOrDefault();
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.ToString();
            }
        }

        public override IDictionary<string, PreValue> GetPreValues(PreValueContext context)
        {
            return base.GetPreValuesInternal(context, 1);
        }
    }
}