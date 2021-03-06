﻿namespace Uximagine.Magicurve.DataTransfer.Common
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Newtonsoft.Json;

    using Uximagine.Magicurve.Core.Shapes;

    /// <summary>
    /// The controls result.
    /// </summary>
    [DataContract]
    [Serializable]
    public class ControlsResult
    {
        /// <summary>
        /// Gets or sets the controls.
        /// </summary>
        /// <value>
        /// The controls.
        /// </value>
        [DataMember]
        [JsonProperty("controls")]
        public List<Control> Controls { get; set; }

        /// <summary>
        /// Gets or sets the width of the source image.
        /// </summary>
        /// <value>
        /// The width of the source image.
        /// </value>
        [DataMember]
        [JsonProperty("imageWidth")]
        public int ImageWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the image.
        /// </summary>
        /// <value>
        /// The height of the image.
        /// </value>
        [DataMember]
        [JsonProperty("imageHeight")]
        public int ImageHeight { get; set; }
    }
}