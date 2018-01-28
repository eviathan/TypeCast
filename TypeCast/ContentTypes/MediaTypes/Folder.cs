using TypeCast;
using TypeCast.DataTypes;
using TypeCast.Attributes;
using TypeCast.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using TypeCast.DataTypes.BuiltIn;
using TypeCast.ContentTypes;

namespace TypeCast.ContentTypes
{
    [CodeFirstCommonBase]
    public abstract class MediaFolderBase : MediaTypeBase
    {
		public class ContentsTab : TabBase
		{
			[ContentProperty(@"Contents:", @"contents", false, @"", 0, false)]
			public virtual TypeCast.DataTypes.BuiltIn.ListView_Media Contents { get; set; }

		}

		[ContentTab(@"Contents", 1)]
		public virtual ContentsTab Contents { get; set; }
	}

    [MediaType("Folder", "Folder", new Type[] { typeof(MediaFolder), typeof(MediaImage), typeof(MediaFile) }, "icon-folder", true, false, "")]
    [BuiltInMediaType]
    public class MediaFolder : MediaFolderBase
    {

    }
}