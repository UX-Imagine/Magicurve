namespace Uximagine.Magicurve.DataTransfer.Common
{
    using System;
    using System.Runtime.Serialization;

    using Newtonsoft.Json;

    /// <summary>
    /// The code result.
    /// </summary>
    [Serializable]
    public class CodeResult
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [DataMember]
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}