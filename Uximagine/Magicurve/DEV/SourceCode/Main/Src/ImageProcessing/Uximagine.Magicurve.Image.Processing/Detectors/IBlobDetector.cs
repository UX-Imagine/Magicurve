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
    public interface IBlobDetector
    {
        /// <summary>
        /// Gets the shape.
        /// </summary>
        /// <returns>
        /// The controls.
        /// </returns>
        List<Control> GetShapes();

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <returns>
        /// The resulting Image.
        /// </returns>
        Bitmap GetImage();

        /// <summary>
        /// Processes the image.
        /// </summary>
        /// <param name="originalImage">
        /// The original image.
        /// </param>
        void ProcessImage(Bitmap originalImage);
    }
}
