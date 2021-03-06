﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using Uximagine.Magicurve.Core.Shapes;

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
        public List<Control> Controls { get; set; }

        /// <summary>
        /// Gets or sets the image result.
        /// </summary>
        /// <value>
        /// The image result.
        /// </value>
        [DataMember]
        public Bitmap ImageResult { get; set; }

        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        /// <value>
        /// The image path.
        /// </value>
        [DataMember]
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the width of the source image.
        /// </summary>
        /// <value>
        /// The width of the source image.
        /// </value>
        [DataMember]
        public int SourceImageWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the source image.
        /// </summary>
        /// <value>
        /// The height of the source image.
        /// </value>
        [DataMember]
        public int SourceImageHeight { get; set; }

    }
}
