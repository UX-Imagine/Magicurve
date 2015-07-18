using AForge;
using System.Collections.Generic;
using Uximagine.Magicurve.Core.Models;

namespace Uximagine.Magicurve.Core.Shapes
{
    /// <summary>
    /// The control function.
    /// </summary>
    public class Control : IControl
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public virtual ControlType Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the edges.
        /// </summary>
        /// <value>
        /// The edges.
        /// </value>
        public List<IntPoint> Edges
        {
            get;
            set;
        }
    }
}
