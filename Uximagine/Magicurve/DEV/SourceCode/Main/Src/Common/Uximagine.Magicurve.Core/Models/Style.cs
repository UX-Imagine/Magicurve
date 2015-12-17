using System;
using System.Runtime.Serialization;

namespace Uximagine.Magicurve.Core.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// The style.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Style
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [DataMember]
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [DataMember]
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Style"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public Style(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Style"/> class.
        /// </summary>
        public Style()
        {
            
        }
    }
}
