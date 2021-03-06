using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using TypeCast.ContentTypes;

namespace TypeCast.Events
{
	public interface IOnDeleteBase { }

	public interface IOnDelete<in T> : IOnDeleteBase where T : CodeFirstContentBase
	{
		bool OnDelete(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext);
	}
}