#region Imports
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.DataTransfer.Responses;
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
        /// <param name="bitmap">
        /// The bitmap.
        /// </param>
        /// <returns>
        /// The Processed output.
        /// </returns>
        public ProcessResponseDto ProcessImage(string path)
        {
            ProcessResponseDto response = new ProcessResponseDto();

            using (Bitmap bitmap = new Bitmap(path))
            {

                IDetector edgeDetector = DetectorFactory.GetEdgeDetector();

                Bitmap edgeResult = edgeDetector.Detect(bitmap);

                IDetector blobDetector = DetectorFactory.GetBlobDetector();

                Bitmap correctFormatImage = edgeResult.ConvertToFormat(PixelFormat.Format24bppRgb);

                Bitmap blobResult = blobDetector.Detect(correctFormatImage);

                response.Image = blobResult;

            }

            return response;
        }
    }
}
