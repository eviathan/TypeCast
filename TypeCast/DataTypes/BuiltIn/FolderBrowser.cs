
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

namespace TypeCast.DataTypes.BuiltIn
{
    [DataType("Umbraco.FolderBrowser", "Folder Browser", null, DatabaseType.Nvarchar)]
    [BuiltInDataType][DoNotSyncDataType]
    public class FolderBrowser : IUmbracoNvarcharDataType
    {
        //TODO implement the properties and serialisation logic for the Umbraco.FolderBrowser property editor's values

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
            return "";
        }
    }
}