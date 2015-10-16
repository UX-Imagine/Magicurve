using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace Uximagine.Magicurve.DataTransfer.Requests
{
    [DataContract]
    [Serializable]
    public class FileUpdateRequest
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        [DataMember]
        public Bitmap Image { get; set; } 
    }
}