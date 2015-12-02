#region Imports
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing.Helpers;
using Point = AForge.Point; 
#endregion

namespace Uximagine.Magicurve.Image.Processing.Detectors
{
    /// <summary>
    ///     The blob detector.
    /// </summary>
    public class BlobDetector : IBlobDetector
    {
        #region Fileds - Instance Members
        /// <summary>
        ///     The BLOB counter.
        /// </summary>
        private BlobCounter _blobCounter;

        /// <summary>
        ///     The controls.
        /// </summary>
        private List<Control> _controls;

        /// <summary>
        ///     The image.
        /// </summary>
        private Bitmap _image;
        #endregion

        #region Properties - Instance Member
        /// <summary>
        ///     Gets or sets the blobs.
        /// </summary>
        /// <value>
        ///     The blobs.
        /// </value>
        public Blob[] Blobs { get; set; }
        #endregion

        #region Methods - Instance Member - Public Members
        /// <summary>
        ///     Processes the image.
        /// </summary>
        /// <param name="originalImage">
        ///     The original image.
        /// </param>
        public void ProcessImage(Bitmap originalImage)
        {
            // step 2 - locating objects
            this._image = originalImage;
            this._blobCounter = this.GetBlobs();
            this.Blobs = _blobCounter.GetObjectsInformation();
        }

        /// <summary>
        ///     Detects the specified original image.
        /// </summary>
        /// <returns>
        ///     The detected image.
        /// </returns>
        public List<Control> GetShapes()
        {
            this.GenerateControls();
            return _controls;
        }

        /// <summary>
        ///     Gets the image.
        /// </summary>
        /// <returns>
        ///     The detected shapes in painted image.
        /// </returns>
        public Bitmap GetImage()
        {
            // step 2 - locating objects
            var g = Graphics.FromImage(this._image);
            var bluePen = new Pen(Color.Blue, 2); //// other

            for (int i = 0, n = this.Blobs.Length; i < n; i++)
            {
                List<IntPoint> edgePoints = _blobCounter.GetBlobsEdgePoints(this.Blobs[i]);
                Pen pen = bluePen;
                g.DrawPolygon(pen, edgePoints.ToPointsArray());
            }
           
            bluePen.Dispose();
            g.Dispose();

            return this._image;
        }
        #endregion

        #region Mehtods - Instance Member - Private Members
        /// <summary>
        ///     Generates the controls.
        /// </summary>
        private void GenerateControls()
        {
            //// step 3 - check objects' type and highlight
            var shapeChecker = ProcessingFactory.GetShapeChecker();

            _controls = new List<Control>();

            for (int i = 0, n = this.Blobs.Length; i < n; i++)
            {
                var edgePoints = this._blobCounter.GetBlobsEdgePoints(this.Blobs[i]);

                Control control = edgePoints.ToControl();

                _controls.Add(control);
            }
        }

        /// <summary>
        /// Gets the blobs.
        /// </summary>
        /// <returns>
        /// The blob counter.
        /// </returns>
        private BlobCounter GetBlobs()
        {
            // lock image
            var bitmapData = this._image.LockBits(
                new Rectangle(0, 0, this._image.Width, this._image.Height),
                ImageLockMode.ReadWrite, this._image.PixelFormat);


            // step 1 - turn background to black
            var colorFilter = new ColorFiltering
            {
                Red = new IntRange(0, 64),
                Green = new IntRange(0, 64),
                Blue = new IntRange(0, 64),
                FillOutsideRange = false
            };

            colorFilter.ApplyInPlace(bitmapData);

            // step 2 - locating objects
            this._blobCounter = new BlobCounter
            {
                FilterBlobs = true,
                MinHeight = 20,
                MinWidth = 20
            };


            this._blobCounter.ProcessImage(bitmapData);
            this._image.UnlockBits(bitmapData);

            return this._blobCounter;
        } 
        #endregion
    }
}