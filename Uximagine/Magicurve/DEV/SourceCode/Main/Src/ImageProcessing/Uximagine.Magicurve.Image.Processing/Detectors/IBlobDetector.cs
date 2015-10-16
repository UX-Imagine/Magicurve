// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBlobDetector.cs" company="ux-imagine">
//   2015 UX-Imagine.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Drawing;
using Uximagine.Magicurve.Core.Shapes;
namespace Uximagine.Magicurve.Image.Processing.Detectors
{
    /// <summary>
    /// The blob detector.
    /// </summary>
    public interface IBlobDetector : IDetector
    {
        /// <summary>
        /// Gets the shape.
        /// </summary>
        /// <param name="originalImage">
        /// The edge points.
        /// </param>
        /// <returns>
        /// The controls.
        /// </returns>
        List<Control> GetShapes(Bitmap originalImage);

    }
}
