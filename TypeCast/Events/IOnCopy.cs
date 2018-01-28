using Umbraco.Core.Models;
using System.Web;
using Umbraco.Web;
using Umbraco.Core;
using TypeCast.ContentTypes;

namespace TypeCast.Events
{
	public interface IOnCopyBase { }

	public interface IOnCopy<in T> : IOnCopyBase where T : CodeFirstContentBase
	{
		bool OnCopy(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext);
	}
}