using AForge.Imaging.Filters;
using System.Drawing;

namespace Uximagine.Magicurve.Image.Processing.Detectors
{
    /// <summary>
    /// The detector for edges.
    /// </summary>
    public class EdgeDetector : IDetector
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

            Bitmap image = Grayscale.CommonAlgorithms.BT709.Apply(originalImage);

            SobelEdgeDetector edgeFilter = new SobelEdgeDetector();
            result = edgeFilter.Apply(image);

            image.Dispose();

            return result;
        }
    }
}
