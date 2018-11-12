using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace TypeCast.Attributes.DataTypes.PreValues
{
    /// <summary>
    /// TODO: Add xml comment
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class NestedContentPreValueAttribute : CodeFirstAttribute, IDataTypeInstance
    {
        public class NestedContentType
        {
            [JsonProperty("ncAlias")]
            public string Alias { get; set; }

            [JsonProperty("ncTabAlias")]
            public string TabAlias { get; set; }

            [JsonProperty("nameTemplate")]
            public string NameTemplate { get; set; }

            [JsonIgnore]
            public Type Type { get; set; }

            public NestedContentType() { }

            public NestedContentType(string documentTypeAlias, string tabAlias, string nameTemplate, Type type)
            {
                TabAlias = tabAlias;
                nameTemplate = documentTypeAlias;
                Alias = Alias;
                Type = type;
            }
        }

        public IEnumerable<NestedContentType> ContentTypes { get; set; }

        public int MinItems { get; set; }

        public int MaxItems { get; set; }

        public bool ConfirmDeletes { get; set; }

        public bool ShowIcons { get; set; }

        public bool HideLabel { get; set; }

        public string TabAlias { get; set; }

        public NestedContentPreValueAttribute(Type[] contentTypes, int minItems = 0, int maxItems = 0, bool confirmDeletes = false, bool showIcons = false, bool hideLabel = false, string tabAlias = null)
        {
            if (!contentTypes.Any()) throw new ArgumentOutOfRangeException($"{nameof(contentTypes)} argument array needs to have at least one type.");

            MinItems = minItems;
            MaxItems = maxItems;
            ConfirmDeletes = confirmDeletes;
            ShowIcons = showIcons;
            HideLabel = hideLabel;
            TabAlias = tabAlias ?? "Content";

            ContentTypes = contentTypes.Select(ConvertTypeToNestedContentType);
        }

        public IDictionary<string, PreValue> GetPrevalueDictionary()
        {
            var output = new Dictionary<string, PreValue>();

            // TODO: De-yuckify this
            output.Add("minItems", new PreValue(MinItems.ToString()));
            output.Add("maxItems", new PreValue(MaxItems.ToString()));
            output.Add("confirmDeletes", new PreValue(Convert.ToByte(ConfirmDeletes).ToString()));
            output.Add("showIcons", new PreValue(Convert.ToByte(ShowIcons).ToString()));
            output.Add("hideLabels", new PreValue(Convert.ToByte(HideLabel).ToString()));
            output.Add("contentTypes", new PreValue(JsonConvert.SerializeObject(ContentTypes)));

            return output;
        }

        private NestedContentType ConvertTypeToNestedContentType(Type type)
        {
            var nestedContentType = new NestedContentType();
            var name = type.Name;

            nestedContentType.Alias = Char.ToLowerInvariant(name[0]) + name.Substring(1);
            nestedContentType.NameTemplate = "{{$index}} - {{ pickerAlias | ncNodeName }}";
            nestedContentType.TabAlias = TabAlias;
            nestedContentType.Type = type;

            return nestedContentType;
        }
    }
}