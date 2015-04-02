#region Imports
using System.Drawing;
using System.Drawing.Imaging;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Image.Processing.Detectors;
using Uximagine.Magicurve.Image.Processing.Helpers;
#endregion

namespace Uximagine.Magicurve.Image.Processing
{
    /// <summary>
    /// The class which handle processing of the image.
    /// </summary>
    public class Processor
    {
        /// <summary>
        /// Gets or sets the shape.
        /// </summary>
        /// <value>
        /// The shape.
        /// </value>
        public ControlType Type 
        { 
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets the shape.
        /// </summary>
        /// <value>
        /// The shape.
        /// </value>
        public IControl Shape
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Processes the image.
        /// </summary>
        /// <param name="path">
        /// The bitmap path.
        /// </param>
        /// <returns>
        /// The Processed output.
        /// </returns>
        public Bitmap ProcessImage(string path)
        {
            Bitmap result = null;

            using (Bitmap bitmap = new Bitmap(path))
            {

                IDetector edgeDetector = DetectorFactory.GetEdgeDetector();

                Bitmap edgeResult = edgeDetector.Detect(bitmap);

                IDetector blobDetector = DetectorFactory.GetBlobDetector();

                Bitmap correctFormatImage = edgeResult.ConvertToFormat(PixelFormat.Format24bppRgb);

                result = blobDetector.Detect(correctFormatImage);

            }

            return result;
        }
    }
}
