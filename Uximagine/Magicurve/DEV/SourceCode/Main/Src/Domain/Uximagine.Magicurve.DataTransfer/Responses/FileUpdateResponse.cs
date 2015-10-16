using System;
using System.Runtime.Serialization;

namespace Uximagine.Magicurve.DataTransfer.Responses
{
    /// <summary>
    /// The file update response.
    /// </summary>
    [DataContract]
    [Serializable]
    public class FileUpdateResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FileUpdateResponse"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool Success { get; set; } 
    }
}