﻿using System.Drawing;

namespace Uximagine.Magicurve.Image.Processing.Detectors
{
    /// <summary>
    /// The detector interface.
    /// </summary>
    public interface IDetector
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
        Bitmap Detect(Bitmap originalImage);
    }
}
