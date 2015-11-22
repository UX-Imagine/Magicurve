using Uximagine.Magicurve.Core.Models;

namespace Uximagine.Magicurve.Core.Shapes
{
    /// <summary>
    /// The Triangle Shape.
    /// </summary>
    public class Label : Control
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public override ControlType Type
        {
            get
            {
                return ControlType.Label;
            }
        }

        public string Value
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
