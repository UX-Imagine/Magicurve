using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
