using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.DataTransfer.Requests
{
    /// <summary>
    /// Generate Code Request.
    /// </summary>
    [Serializable]
    [DataContract]
    public class GenerateCodeRequest
    {
        /// <summary>
        /// Gets or sets the controls.
        /// </summary>
        /// <value>
        /// The controls.
        /// </value>
        [DataMember]
        public List<Control> Controls { get; set; }

        /// <summary>
        /// Gets or sets the width of the image.
        /// </summary>
        /// <value>
        /// The width of the image.
        /// </value>
        [DataMember]
        public int ImageWidth { get; set; }
    }
}
