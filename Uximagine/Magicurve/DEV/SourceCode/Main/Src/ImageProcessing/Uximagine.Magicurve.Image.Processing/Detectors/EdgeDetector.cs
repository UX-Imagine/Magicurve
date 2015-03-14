using AForge.Imaging.Filters;
using System.Drawing;

namespace Uximagine.Magicurve.Image.Processing.Detectors
{
    /// <summary>
    /// The detector for edges.
    /// </summary>
    internal class EdgeDetector : IDetector
    {
        /// <summary>
        /// Detects the specified original image.
        /// </summary>
        /// <param name="originalImage">
        /// The original image.
        /// </param>
        /// <returns>
        /// The detected image.
        /// </returns>
        public Bitmap Detect(Bitmap originalImage)
        {
            Bitmap result = null;

            Bitmap gsImage = Grayscale.CommonAlgorithms.BT709.Apply(originalImage);
            CannyEdgeDetector filter = new CannyEdgeDetector();
            result = filter.Apply(gsImage);
            gsImage.Dispose();
            
            return result;
        }
    }
}
