using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeCast.ContentTypes;
using TypeCast.Core.Modules;
using Umbraco.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using TypeCast.Extensions;
using TypeCast.ContentTypes.DocumentTypes;
using TypeCast.DataTypes.BuiltIn;
using Umbraco.Core.Logging;

namespace TypeCast.Converters
{
    // NOTE: DOOES THIS HIT THE DATABASE BECAUSE IT SHOULDNT
    /// <summary>
    /// Serialises and Deserialises Nested Content to TypeCast POCOs
    /// </summary>
    public class NestedContentJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(typeof(NestedContentItem));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties().ToList();

            Dictionary<string, object> contentProperties = properties.Where(x => !new string[] { }
                                                                     .Contains(x.Name))
                                                                     .ToDictionary(x => x.Name, x => ((JValue)x.Value)
                                                                     .Value);

            var contentTypeAlias = contentProperties["ncContentTypeAlias"].ToString();

         
            // Create an empty content with type derived from contentTypeAlias
            var content = ApplicationContext.Current.Services.ContentService.CreateContent($"tmpItem{DateTime.Now}", -1, contentTypeAlias);            

            // Iterate over all properties on content type and set them with the values stored in the Json Object
            foreach (var property in properties)
            {
                if (content.Properties.Any(x => x.Alias == property.Name))
                {
                    content.Properties[property.Name].Value = contentProperties[property.Name];
                }
                else
                {
                    LogHelper.Warn(typeof(NestedContentJsonConverter), $"Could not find property with alias {property.Name}");
                }
            }

            var typedContent = content.ConvertToModel() as DocumentTypeBase;

            // Return Codefirst POCO            
            return new NestedContentItem
            {
                Key = contentProperties["key"]?.ToString() ?? string.Empty,
                Name = contentProperties["name"]?.ToString() ?? string.Empty,
                NcContentTypeAlias = contentTypeAlias,
                Value = typedContent
            };
        }

        // TODO: Implement this
        // The json data structure is somewhat denoramlised which makes this a little less simple than a straight
        // serialisation using the built in serialisation method.
        // Look in the db but the document properties are stored on the top level object with the property naming convention being "tabName_propertyName"
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}