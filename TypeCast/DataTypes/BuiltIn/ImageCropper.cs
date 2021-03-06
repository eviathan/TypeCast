
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
using Umbraco.Web;
using System;
using TypeCast.Core;
using Newtonsoft.Json;
using Umbraco.Web.Models;
using System.Web;
using System.Runtime.Serialization;
using System.Reflection;
using System.Drawing;
using System.Web.Mvc;

namespace TypeCast.DataTypes.BuiltIn
{
    [DataType("Umbraco.ImageCropper", "Image Cropper")]
    [BuiltInDataType][DoNotSyncDataType]
    public class ImageCropper : IUmbracoNtextDataType, IPreValueFactory, IHtmlString
    {
        [JsonIgnore]
        private ImageCropDataSet _underlying = new ImageCropDataSet();

		public static implicit operator ImageCropper(ImageCropDataSet cropData)
		{
			var result = new ImageCropper();
			result.Initialise(JsonConvert.SerializeObject(cropData));
			return result;
		}

        public ImageCropper()
        {
            Effects = new CropEffects();
        }

        public ImageCrop this[string alias]
        {
            get
            {
                return Crops.FirstOrDefault(x => x.Alias == alias);
            }
        }

        public ImageCropFocalPoint FocalPoint { get; set; }

        public List<ImageCrop> Crops { get; set; }

        [JsonIgnore]
        public CropEffects Effects { get; set; }

        [JsonProperty(PropertyName="src")]
        public string OriginalImageUrl { get; set; }

        /// <summary>
        /// Initialises the instance from the db value
        /// </summary>
        public void Initialise(string dbValue)
        {
			if (!string.IsNullOrWhiteSpace(dbValue))
			{
				JsonConvert.PopulateObject(dbValue, this);
				_underlying = JsonConvert.DeserializeObject<ImageCropDataSet>(dbValue); //This is useful as it has logic for generating the crop URLs
				Effects.SetUnderlyingDataset(_underlying);
				if (Crops == null)
				{
					Crops = new List<ImageCrop>();
				}
				Crops.ForEach(x => x.SetUnderlyingDataset(_underlying));
				var crops = GetCropsFromProperties();
				foreach (var crop in crops)
				{
					crop.Key.SetValue(this, Crops.FirstOrDefault(x => x.Alias == crop.Value.Alias));
				}
			}
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public string Serialise()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public IDictionary<string, PreValue> GetPreValues(PreValueContext context)
        {
            var result = new Dictionary<string, PreValue>();
            var propAttrs = context.CurrentMember.GetCodeFirstAttributes<ImageCropAttribute>().Select(x => new ImageCropDefinition() { Alias = x.Alias, Height = x.Height, Width = x.Width });
            var typeAttrs = this.GetType().GetCodeFirstAttributes<ImageCropAttribute>().Select(x => new ImageCropDefinition() { Alias = x.Alias, Height = x.Height, Width = x.Width }); ;
            var typePropAttrs = GetCropsFromProperties();

            var attrs = propAttrs.Union(typePropAttrs.Values).Union(typeAttrs).ToArray();
            result.Add("crops", new PreValue(JsonConvert.SerializeObject(attrs)));
            return result;
        }

        private Dictionary<PropertyInfo, ImageCropDefinition> GetCropsFromProperties()
        {
            var typePropAttrs = this.GetType()
                                        .GetProperties()
                                        .Where(x => x.PropertyType == typeof(ImageCrop) && x.GetCodeFirstAttribute<ImageCropPropertyAttribute>() != null)
                                        .ToDictionary(x => x, x =>
                                            {
                                                var attr = x.GetCodeFirstAttribute<ImageCropPropertyAttribute>();
                                                return new ImageCropDefinition() { Alias = x.Name, Height = attr.Height, Width = attr.Width };
                                            });
            return typePropAttrs;
        }

        public string ToHtmlString()
        {
            var toAdd = DataTypeUtils.GetHtmlTagContentFromContextualAttributes(this);
            return "<img" + toAdd + " src='" + OriginalImageUrl + "' />";
        }

        public override string ToString()
        {
            return OriginalImageUrl;
        }
    }
}