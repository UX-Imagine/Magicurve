using AForge;
using AForge.Math.Geometry;
using System.Collections.Generic;

namespace Uximagine.Magicurve.Image.Processing.ShapeCheckers
{
    /// <summary>
    /// The advanced shape checker.
    /// </summary>
    public class AdvancedShapeChecker : SimpleShapeChecker
    {
        /// <summary>
        /// Determines whether the specified edge points is button.
        /// </summary>
        /// <param name="edgePoints">
        /// The edge points.
        /// </param>
        /// <param name="corners">
        /// The corners.
        /// </param>
        /// <returns>
        /// <c> true</c> if [is button] shape.
        /// </returns>
        public bool IsButton(List<IntPoint> edgePoints, List<IntPoint> corners)
        {
            bool isButton = false;

            

            if (this.CheckIfPointsFitShape(edgePoints, corners))
            {
                if (corners.Count == 4)
                {
                    isButton = true;
                }
            }

            return isButton;
        }

        /// <summary>
        /// Determines whether [is drop down] [the specified edge points].
        /// </summary>
        /// <param name="edgePoints">The edge points.</param>
        /// <param name="corners">The corners.</param>
        /// <returns><c> true</c> if drop down.</returns>
        public bool IsDropDown(List<IntPoint> edgePoints, List<IntPoint> corners)
        {
            bool isDropDown = false;

            if (this.CheckIfPointsFitShape(edgePoints, corners))
            {
                if (corners.Count == 5)
                {
                    isDropDown = true;
                }
            }

            return isDropDown;
        }
    }
}
