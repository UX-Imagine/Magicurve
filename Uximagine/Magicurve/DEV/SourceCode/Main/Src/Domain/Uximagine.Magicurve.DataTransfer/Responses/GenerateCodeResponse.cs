using System;
using System.Runtime.Serialization;

namespace Uximagine.Magicurve.DataTransfer.Responses
{
    /// <summary>
    /// Generate code response.
    /// </summary>
    [Serializable]
    [DataContract]
    public class GenerateCodeResponse
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [DataMember]
        public string Code { get; set; }
    }
}
