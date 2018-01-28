using System;
using System.Linq;
using System.Collections.Generic;
using TypeCast.ContentTypes;
using TypeCast.Core;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace TypeCast.Events
{
	public interface IOnLoadBase { }

	public interface IOnLoad<in T> : IOnLoadBase where T : CodeFirstContentBase
	{
		void OnLoad(T model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage);
	}

	public interface IOnLoad<in T, Tviewmodel> : IOnLoadBase where T : CodeFirstContentBase
	{
		void OnLoad(T model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage, out Tviewmodel viewModel);
	}
}