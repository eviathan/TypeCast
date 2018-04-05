using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace TypeCast.Models
{
    public class UmbracoViewModel<T> : RenderModel
    {
        public UmbracoViewModel(T document) :
            base(UmbracoContext.Current.PublishedContentRequest.PublishedContent)
        {
            Document = document;
        }

        public T Document { get; private set; }
    }
}
