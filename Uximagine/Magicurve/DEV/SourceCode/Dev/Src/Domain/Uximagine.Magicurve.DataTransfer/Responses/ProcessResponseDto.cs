using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace Uximagine.Magicurve.DataTransfer.Responses
{
    /// <summary>
    /// The processed image response.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProcessResponseDto
    {
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        [DataMember]
        public Bitmap Image { get; set; }

        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        /// <value>
        /// The image path.
        /// </value>
        [DataMember]
        public string ImagePath { get; set; }

    }
}
