#region Imports
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;
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

        public Bitmap ImageResult
        {
            get; set;
        }

        public List<Control> Controls
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
        public void ProcessImage(string path)
        {
            using (Bitmap bitmap = new Bitmap(path))
            {

                IDetector edgeDetector = ProcessingFactory.GetEdgeDetector();

                Bitmap edgeResult = edgeDetector.GetImage(bitmap);

                IBlobDetector blobDetector = ProcessingFactory.GetBlobDetector();

                ////Threshold filterThreshold = new Threshold();
                ////filterThreshold.ApplyInPlace(edgeResult);

                Bitmap correctFormatImage = edgeResult.ConvertToFormat(PixelFormat.Format24bppRgb);

                blobDetector.ProcessImage(correctFormatImage);
                this.Controls= blobDetector.GetShapes();
                this.ImageResult = blobDetector.GetImage();
            }
        }
    }
}
