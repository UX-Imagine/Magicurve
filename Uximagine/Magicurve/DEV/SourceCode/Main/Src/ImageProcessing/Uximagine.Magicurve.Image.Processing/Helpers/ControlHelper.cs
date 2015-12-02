using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Math.Geometry;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.Image.Processing.Helpers
{
    /// <summary>
    /// The control helper.
    /// </summary>
    public static class ControlHelper
    {
        /// <summary>
        /// To the control.
        /// </summary>
        /// <param name="edgePoints">The edge points.</param>
        /// <returns>
        /// The control object.
        /// </returns>
        public static Control ToControl(this List<IntPoint> edgePoints)
        {
            Control control = new Control();

            IntPoint minXy;
            IntPoint maxXy;
            PointsCloud.GetBoundingRectangle(edgePoints, out minXy, out maxXy);

            control.X = minXy.X;
            control.Y = minXy.Y;
            control.Height = maxXy.Y - minXy.Y;
            control.Width = maxXy.X - minXy.X;
            control.Type = ControlType.None;
            control.EdgePoints = edgePoints;

            return control;
        }
    }
}
