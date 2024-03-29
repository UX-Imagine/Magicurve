﻿using AForge.Imaging.Filters;
using System.Drawing;

namespace Uximagine.Magicurve.Image.Processing.Detectors
{
    /// <summary>
    /// The detector for edges.
    /// </summary>
    public class SobelDetector : IEdgeDetector
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

            SobelEdgeDetector edgeFilter = new SobelEdgeDetector();
            result = edgeFilter.Apply(image);

            image.Dispose();

            return result;
        }
    }
}
