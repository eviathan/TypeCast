
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
using Umbraco.Web;
using Newtonsoft.Json;
using System.Web;

namespace TypeCast.DataTypes.BuiltIn
{
    [DataType("Umbraco.RelatedLinks", "Related Links")]
    [DoNotSyncDataType][BuiltInDataType]
    public class RelatedLinks : UmbracoJsonCollectionDataType<RelatedLink>
    {
		public static implicit operator RelatedLinks(RelatedLink[] values)
		{
			return new RelatedLinks() { Items = new List<RelatedLink>(values) };
		}
		
		public override void Initialise(string dbValue)
        {
            base.Initialise(dbValue);
            foreach (var item in Items)
            {
                item.SetParent(this);
            }
        }
    }



}