using Umbraco.Core.Models;
using System.Web;
using Umbraco.Web;
using Umbraco.Core;
using TypeCast.ContentTypes;
using Umbraco.Core.Events;

namespace TypeCast.Events
{
	public interface IOnPublishBase { }

	public interface IOnPublish<in T> : IOnPublishBase where T : CodeFirstContentBase
	{
		bool OnPublish(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e);
	}
}