using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using Umbraco.Web;
using Umbraco.Core.Models;
using Umbraco.Core;
using TypeCast.ContentTypes;
using TypeCast.Extensions;
using TypeCast.Attributes;
using System.Reflection;
using TypeCast.Exceptions;
using TypeCast.Core;

namespace TypeCast.Events
{
	public interface IOnCreateBase { }

	public interface IOnCreate<in T> : IOnCreateBase where T : CodeFirstContentBase
	{
		bool OnCreate(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext);
	}

}