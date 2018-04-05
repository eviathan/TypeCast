using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeCast.ContentTypes;

namespace TypeCast.Attributes.ContentTypes
{
    public class NestedContentAttribute : DocumentTypeAttribute
    {
        public NestedContentAttribute(string name = null, 
                                      string alias = null,
                                      Type[] allowedChildren = null, 
                                      string icon = BuiltInIcons.IconDocument,
                                      bool allowAtRoot = false,
                                      bool enableListView = false,
                                      string description = "",
                                      UmbracoIconColor iconColor = UmbracoIconColor.Black) 
            : base(name, alias, allowedChildren, icon, allowAtRoot, enableListView, description)
        {
        }
    }
}
