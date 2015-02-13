using AForge;
using System.Collections.Generic;
using Uximagine.Magicurve.Core.Models;

namespace Uximagine.Magicurve.Core.Shapes

{
    /// <summary>
    /// The Triangle Shape.
    /// </summary>
    public class Button : Control
    {
        public override ControlType Type
        {
            get
            {
                return ControlType.Button;
            }
        }
    }
}
