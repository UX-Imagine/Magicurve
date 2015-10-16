using AForge;
using System.Collections.Generic;

namespace Uximagine.Magicurve.Core.Models
{
    /// <summary>
    /// The control interface.
    /// </summary>
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
    }
}
