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

namespace TypeCast.Converters
{
    /// <summary>
    /// Serialises and Deserialises Nested Content to TypeCast POCOs
    /// </summary>
    public class NestedContentJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties().ToList();
            Dictionary<string, object> contentProperties = properties.Where(x => !new string[] { }.Contains(x.Name)).ToDictionary(x => x.Name, x => ((JValue)x.Value).Value);
            var contentTypeAlias = contentProperties["ncContentTypeAlias"].ToString(); // TODO: Nullcheck make pretty

         
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
                    // TODO: Log 
                }
            }

            // TODO: Convert to codefirst POCO
            var typedContent = content.ConvertToModel() as DocumentTypeBase; // NOTE: WE NEED THE TYPE HERE!!!!!

            // Return Codefirst POCO            
            return new NestedContentItem
            {
                Key = contentProperties["key"]?.ToString() ?? string.Empty,
                Name = contentProperties["name"]?.ToString() ?? string.Empty,
                NcContentTypeAlias = contentTypeAlias,
                Value = typedContent
            };

            // NOTES:
            // 1. GET ALL Properties on class or nested content tab classes with contenttype attributes on them that implement IUmbracoDataType<T>
            // 1. Pass value stored in property to the Initialise method on property

            // I AM OVERTHINKING THIS WE CAN PROBABLY WORK FROM THE IPUBLISHED CONTENT PROPERTY VALUES



            //ContentTypeRegistration docType;
            //if (_contentTypeModule.TryGetContentType(content.DocumentTypeAlias, out docType))
            //{
            //    MethodInfo convertToModel = GetConvertToModelMethod(docType.ClrType);
            //    return convertToModel.Invoke(this, new object[] { content, parentContext });
            //}

            //var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            //umbHelper.type
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}