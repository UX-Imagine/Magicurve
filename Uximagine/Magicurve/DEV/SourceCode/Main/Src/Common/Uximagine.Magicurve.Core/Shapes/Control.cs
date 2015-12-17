using System.Collections.Generic;
using System.Runtime.Serialization;
using AForge;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Uximagine.Magicurve.Core.Models;

namespace Uximagine.Magicurve.Core.Shapes
{
    /// <summary>
    /// The control function.
    /// </summary>
    [DataContract]
    public class Control : IControl
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual ControlType Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        [DataMember]
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        [DataMember]
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        [DataMember]
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        [DataMember]
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the edge points.
        /// </summary>
        /// <value>
        /// The edge points.
        /// </value>
        [JsonIgnore]
        public List<IntPoint> EdgePoints
        {
            get; set;
        } 

        /// <summary>
        /// Gets or sets the styles.
        /// </summary>
        /// <value>
        /// The styles.
        /// </value>
        [DataMember]
        public List<Style> Styles
        {
            get;
            set;
        }

    }
}
