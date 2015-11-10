using AForge;
using AForge.Math.Geometry;
using System.Collections.Generic;
using Uximagine.Magicurve.Core.Models;

namespace Uximagine.Magicurve.Image.Processing.ShapeCheckers
{
    /// <summary>
    /// The advanced shape checker.
    /// </summary>
    public abstract class AdvancedShapeChecker : SimpleShapeChecker, IShapeChecker
    {
        /// <summary>
        /// The shape optimizer.
        /// </summary>
        private readonly FlatAnglesOptimizer _shapeOptimizer = new FlatAnglesOptimizer(160f);

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public double Height { get; set; }

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
            return this._shapeOptimizer.OptimizeShape(PointsCloud.FindQuadrilateralCorners((IEnumerable<IntPoint>)edgePoints));
        }
        
    }
}
