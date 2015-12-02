#region Imports
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing.Detectors;
using Uximagine.Magicurve.Image.Processing.Helpers;
using Uximagine.Magicurve.Image.Processing.ShapeCheckers;

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
                // Apply filters to get smooth image.
                Bitmap blobReady = bitmap.GetBlobReady();

                // Detect Shapes.
                IBlobDetector blobDetector = ProcessingFactory.GetBlobDetector();
                blobDetector.ProcessImage(blobReady);

                List<Control> controls = blobDetector.GetShapes();
                this.ImageResult = blobDetector.GetImage();

                // Identify control type.
                IShapeChecker shapeChecker = ProcessingFactory.GetShapeChecker();

                controls =
                    controls.Where(c => c.Width > ConfigurationData.MinSize && c.Height > ConfigurationData.MinSize)
                        .ToList();

                foreach (Control control in controls)
                {
                    ControlType type = shapeChecker.GetControlType(blobReady, control.EdgePoints);
                    control.Type = type;
                }

                Controls = controls;
            }
        }
    }
}
