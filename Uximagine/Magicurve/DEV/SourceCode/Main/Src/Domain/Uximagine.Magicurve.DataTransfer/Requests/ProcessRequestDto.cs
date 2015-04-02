using System;
using System.Runtime.Serialization;

namespace Uximagine.Magicurve.DataTransfer.Requests
{
    /// <summary>
    /// The request (DTO) for process image.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProcessRequestDto
    {
        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        /// <value>
        /// The image path.
        /// </value>
        [DataMember]
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the server path.
        /// </summary>
        /// <value>
        /// The server path.
        /// </value>
        [DataMember]
        public string ServerPath { get; set; }
    }
}
