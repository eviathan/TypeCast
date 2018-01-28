﻿using TypeCast;
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

namespace TypeCast.DataTypes.BuiltIn
{
	[DataType("Umbraco.ListView", "List View - Media")]
	[DoNotSyncDataType][BuiltInDataType]
	public class ListView_Media : IUmbracoNvarcharDataType
	{
		//TODO implement the properties and serialisation logic for the Umbraco.ListView property editor's values

		/// <summary>
		/// Initialises the instance from the db value
		/// </summary>
		public void Initialise(string dbValue)
		{
			
		}

		/// <summary>
		/// Serialises the instance to the db value
		/// </summary>
		public string Serialise()
		{
			return string.Empty;
		}
	}
}