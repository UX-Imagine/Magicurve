﻿using AForge.Imaging.Filters;
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

            Bitmap gsImage = Grayscale.CommonAlgorithms.BT709.Apply(originalImage);
            
            ContrastStretch filter = new ContrastStretch();            
            filter.ApplyInPlace(gsImage);

            CannyEdgeDetector edgeFilter = new CannyEdgeDetector();
            result = edgeFilter.Apply(gsImage);

            gsImage.Dispose();
            
            return result;
        }
    }
}
