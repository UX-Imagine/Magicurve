
using AForge;
using System.Collections.Generic;

namespace Uximagine.Magicurve.Core.Models
{
    public interface IControl
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        ControlType Type
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
        List<IntPoint> Edges
        { 
            get; 
            set; 
        }
    }
}
