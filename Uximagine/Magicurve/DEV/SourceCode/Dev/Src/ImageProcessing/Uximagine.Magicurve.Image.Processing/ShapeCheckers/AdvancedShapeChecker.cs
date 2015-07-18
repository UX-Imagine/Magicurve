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
        /// The shape optimizer.
        /// </summary>
        private readonly FlatAnglesOptimizer shapeOptimizer = new FlatAnglesOptimizer(160f);

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

        /// <summary>
        /// Gets the shape corners.
        /// </summary>
        /// <param name="edgePoints">The edge points.</param>
        /// <returns>
        /// The corners.
        /// </returns>
        public List<IntPoint> GetShapeCorners(List<IntPoint> edgePoints)
        {
            return this.shapeOptimizer.OptimizeShape(PointsCloud.FindQuadrilateralCorners((IEnumerable<IntPoint>)edgePoints));
        }
        
    }
}
