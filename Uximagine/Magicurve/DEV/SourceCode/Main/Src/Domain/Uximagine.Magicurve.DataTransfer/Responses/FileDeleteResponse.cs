using System.Runtime.Serialization;

namespace Uximagine.Magicurve.DataTransfer.Responses
{
    [DataContract]
    [System.Serializable]
    public class FileDeleteResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FileDeleteResponse"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool Success { get; set; } 
    }
}