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

namespace TypeCast.DataTypes.BuiltIn
{
    /// <summary>
    /// Nested Content Built-in Umbraco DocumentType
    /// </summary>
    [DataType("Umbraco.NestedContent", "Nested Content")]
    [BuiltInDataType]
    public class NestedContent : ICollection<NestedContentItem>, IEnumerable<NestedContentItem>, IUmbracoNtextDataType
    {
        private List<NestedContentItem> _nestedContentItems { get; set; } = new List<NestedContentItem>();

        /// <summary>
        /// Returns the a collection of documents
        /// </summary>
        public IEnumerable<DocumentTypeBase> Items => _nestedContentItems.Select(x => x.Value);

        /// <summary>
        /// Returns the size of the collection
        /// </summary>
        public int Count => _nestedContentItems.Count;

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
                return _nestedContentItems[index];
            }
            set
            {
                _nestedContentItems[index] = value;
            }
        }

        #region Serialisation and Deserialisation Methods
        /// <summary>
        /// Initialises the instance from the db value
        /// </summary>
        public void Initialise(string dbValue)
        {
            if (string.IsNullOrWhiteSpace(dbValue)) return;

            _nestedContentItems = JsonConvert.DeserializeObject<IEnumerable<NestedContentItem>>(dbValue).ToList();
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public string Serialise()
        {
            return JsonConvert.SerializeObject(_nestedContentItems);
        }
        #endregion

        #region ICollection/ IEnumberable Methods
        /// <summary>
        /// Adds an item to the collection
        /// </summary>
        /// <param name="item"></param>
        public void Add(NestedContentItem item)
        {
            _nestedContentItems.Add(item);
        }

        /// <summary>
        /// Clears the collection
        /// </summary>
        public void Clear()
        {
            _nestedContentItems.Clear();
        }

        /// <summary>
        /// Checks to see if an item exists in the collection
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(NestedContentItem item)
        {
            return _nestedContentItems.Contains(item);
        }

        /// <summary>
        /// Copies an item to a specified index in the collection
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(NestedContentItem[] array, int arrayIndex)
        {
            _nestedContentItems.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the item from the collection
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(NestedContentItem item)
        {
            return _nestedContentItems.Remove(item);
        }

        /// <summary>
        /// Gets the Enumerator for the collection
        /// </summary>
        /// <returns></returns>
        public IEnumerator<NestedContentItem> GetEnumerator()
        {
            return _nestedContentItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _nestedContentItems.GetEnumerator();
        }
        #endregion
    }
    
    /// <summary>
    /// Nested Content Item Data Model used for serialising/ deserialising nested content values for pesistance.
    /// </summary>
    [JsonConverter(typeof(NestedContentJsonConverter))]
    public class NestedContentItem
    {
        /// <summary>
        /// Unique identifier for nested content item
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// Name of nested content item (format specified by template prevalue)
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The nested content items DocumentType alias
        /// </summary>
        [JsonProperty("ncContentTypeAlias")]
        public string NcContentTypeAlias { get; set; }
        
        /// <summary>
        /// The nested Document value
        /// </summary>
        public DocumentTypeBase Value { get; set; }
    }
}
