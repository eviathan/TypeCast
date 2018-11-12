using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeCast.Attributes;
using TypeCast.ContentTypes;
using System.Collections;
using TypeCast.Converters;
using TypeCast.ContentTypes.DocumentTypes;

// TODO: CLEAN THIS MESS UP
namespace TypeCast.DataTypes.BuiltIn
{
    [DataType("Umbraco.NestedContent", "Nested Content")]
    //[DoNotSyncDataType]
    [BuiltInDataType]
    public class NestedContent : ICollection<NestedContentItem>, IEnumerable<NestedContentItem>, IUmbracoNtextDataType
    {
        private List<NestedContentItem> _items { get; set; } = new List<NestedContentItem>();

        /// <summary>
        /// Returns the size of the collection
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Returns whether or not the collection is read only
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Defines the indexer for the collection
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public NestedContentItem this[int index]
        {
            get
            {
                return _items[index];
            }
            set
            {
                _items[index] = value;
            }
        }

        #region Serialisation and Deserialisation Methods
        /// <summary>
        /// Initialises the instance from the db value
        /// </summary>
        public void Initialise(string dbValue)
        {
            if (string.IsNullOrWhiteSpace(dbValue)) return;

            _items = JsonConvert.DeserializeObject<IEnumerable<NestedContentItem>>(dbValue).ToList();
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public string Serialise()
        {
            return JsonConvert.SerializeObject(_items);
        }
        #endregion

        #region ICollection/ IEnumberable Methods
        /// <summary>
        /// Adds an item to the collection
        /// </summary>
        /// <param name="item"></param>
        public void Add(NestedContentItem item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Clears the collection
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }

        /// <summary>
        /// Checks to see if an item exists in the collection
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(NestedContentItem item)
        {
            return _items.Contains(item);
        }

        /// <summary>
        /// Copies an item to a specified index in the collection
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(NestedContentItem[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the item from the collection
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(NestedContentItem item)
        {
            return _items.Remove(item);
        }

        /// <summary>
        /// Gets the Enumerator for the collection
        /// </summary>
        /// <returns></returns>
        public IEnumerator<NestedContentItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        #endregion
    }
    
    // TODO: Move these into their own files
    [JsonConverter(typeof(NestedContentJsonConverter))]
    public class NestedContentItem
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ncContentTypeAlias")]
        public string NcContentTypeAlias { get; set; }
        
        public DocumentTypeBase Value { get; set; }
    }
}
