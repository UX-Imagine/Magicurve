using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.UI.Web.Models
{
    /// <summary>
    /// The images result.
    /// </summary>
    [Serializable]
    [DataContract]
    public class ImagesResult
    {
        /// <summary>
        /// Gets or sets the upload URL.
        /// </summary>
        /// <value>
        /// The upload URL.
        /// </value>
        [DataMember]
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the width of the source image.
        /// </summary>
        /// <value>
        /// The width of the source image.
        /// </value>
        [DataMember]
        [JsonProperty("imageWidth")]
        public int SourceImageWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the image.
        /// </summary>
        /// <value>
        /// The height of the image.
        /// </value>
        [DataMember]
        [JsonProperty("imageHeight")]
        public int SourceImageHeight { get; set; }

        /// <summary>
        /// Gets or sets the controls.
        /// </summary>
        /// <value>
        /// The controls.
        /// </value>
        [DataMember]
        [JsonProperty("controls")]
        public List<Control> Controls { get; set; }
    }
}