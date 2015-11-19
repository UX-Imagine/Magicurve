// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CannyDetector.cs" company="Uximagine">
//   Uximagine 2015.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Uximagine.Magicurve.Image.Processing.Detectors
{
    using System.Drawing;

    using AForge.Imaging.Filters;

    /// <summary>
    /// The canny detector.
    /// </summary>
    public class CannyDetector : IEdgeDetector
    {
        /// <summary>
        /// Detects the specified original image.
        /// </summary>
        /// <param name="originaImage">
        /// The original image.
        /// </param>
        /// <returns>
        /// The detected image.
        /// </returns>
        public Bitmap GetImage(Bitmap originaImage)
        {

            Bitmap result = null;

            Bitmap image = Grayscale.CommonAlgorithms.BT709.Apply(originaImage);

            CannyEdgeDetector edgeFilter = new CannyEdgeDetector(15, 100);

            result = edgeFilter.Apply(image);

            image.Dispose();

            return result;
        }
    }
}
