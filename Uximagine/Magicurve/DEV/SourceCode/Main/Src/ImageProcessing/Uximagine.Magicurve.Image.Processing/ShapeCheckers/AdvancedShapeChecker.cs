using AForge;
using AForge.Math.Geometry;
using System.Collections.Generic;
using Uximagine.Magicurve.Core.Models;

namespace Uximagine.Magicurve.Image.Processing.ShapeCheckers
{
    /// <summary>
    /// The advanced shape checker.
    /// </summary>
    public abstract class AdvancedShapeChecker : SimpleShapeChecker
    {
        /// <summary>
        /// Gets the type of the control.
        /// </summary>
        /// <param name="edgePoints">
        /// The edge points.
        /// </param>
        /// <returns>
        /// The content type
        /// </returns>
        public abstract ControlType GetControlType(List<IntPoint> edgePoints);
        
    }
}
