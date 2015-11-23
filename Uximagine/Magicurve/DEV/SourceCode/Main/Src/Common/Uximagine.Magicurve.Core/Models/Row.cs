using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.Core.Models
{
    /// <summary>
    /// The controls in a html row is nested as a row object.
    /// </summary>
    [DataContract]
    [Serializable]
    public class Row
    {
        /// <summary>
        /// Gets or sets the controls.
        /// </summary>
        /// <value>
        /// The controls.
        /// </value>
        [DataMember]
        public List<Control> Controls
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        [DataMember]
        public double Height
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the index of the row.
        /// </summary>
        /// <value>
        /// The index of the row.
        /// </value>
        [DataMember]
        public int RowIndex
        {
            get;
            set;
        }
    }
}
