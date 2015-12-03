using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Serialization;
using Uximagine.Magicurve.Core.Shapes;
using Newtonsoft.Json;

namespace Uximagine.Magicurve.UI.Web.Models
{
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
        [JsonProperty("image_width")]
        public int SourceImageWidth { get; set; }
    }
}