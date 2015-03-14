using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
        public Bitmap Image { get; set; }

    }
}
