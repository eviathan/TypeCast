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
using System.Web;
using TypeCast.Core;

namespace TypeCast.ContentTypes
{
    [MediaType("File", "File", null, "icon-document", false, false, "")]
    [BuiltInMediaType]
    public class MediaFile : MediaFileBase
    {

    }

    [CodeFirstCommonBase]
    public class MediaFileBase : MediaTypeBase, IHtmlString
    {
        public class FileTab : TabBase
        {
            [ContentProperty("Upload file", "umbracoFile", false, "", 0, false)]
            public Upload UploadFile { get; set; }

            [ContentProperty("Type", "umbracoExtension", false, "", 1, false)]
            public Label Type { get; set; }

            [ContentProperty("Size", "umbracoBytes", false, "", 2, false)]
            public Label Size { get; set; }
        }

        [ContentTab("File", 1)]
        public FileTab File { get; set; }

        public string ToHtmlString()
        {
            var toAdd = DataTypeUtils.GetHtmlTagContentFromContextualAttributes(this);
            return File == null || File.UploadFile == null ? string.Empty : "<a" + toAdd + " href='" + File.UploadFile.Url + "'>" + NodeDetails.Name + "</a>";
        }

        public override string ToString()
        {
            return File == null || File.UploadFile == null ? string.Empty : File.UploadFile.Url;
        }
    }
}