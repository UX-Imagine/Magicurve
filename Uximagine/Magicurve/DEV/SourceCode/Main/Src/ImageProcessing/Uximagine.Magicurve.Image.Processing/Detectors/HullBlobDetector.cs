// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HullBlobDetector.cs" company="Uximagine">
//   UX-imagine 2015.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using AForge;
using AForge.Imaging;
using AForge.Math.Geometry;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing.Helpers;

namespace Uximagine.Magicurve.Image.Processing.Detectors
{
    /// <summary>
    ///     The hull blob detector.
    /// </summary>
    public class HullBlobDetector : IBlobDetector
    {
        /// <summary>
        ///     The blob counter.
        /// </summary>
        private readonly BlobCounter _blobCounter = new BlobCounter();

        /// <summary>
        ///     The bottom edges.
        /// </summary>
        private readonly Dictionary<int, List<IntPoint>> _bottomEdges = new Dictionary<int, List<IntPoint>>();

        /// <summary>
        ///     The hulls.
        /// </summary>
        private readonly Dictionary<int, List<IntPoint>> _hulls = new Dictionary<int, List<IntPoint>>();

        /// <summary>
        ///     The left edges.
        /// </summary>
        private readonly Dictionary<int, List<IntPoint>> _leftEdges = new Dictionary<int, List<IntPoint>>();

        /// <summary>
        ///     The quadrilaterals.
        /// </summary>
        private readonly Dictionary<int, List<IntPoint>> _quadrilaterals = new Dictionary<int, List<IntPoint>>();

        /// <summary>
        ///     The right edges.
        /// </summary>
        private readonly Dictionary<int, List<IntPoint>> _rightEdges = new Dictionary<int, List<IntPoint>>();

        /// <summary>
        ///     The top edges.
        /// </summary>
        private readonly Dictionary<int, List<IntPoint>> _topEdges = new Dictionary<int, List<IntPoint>>();

        /// <summary>
        ///     The Blobs.
        /// </summary>
        private Blob[] _blobs;

        /// <summary>
        ///     The originaImage.
        /// </summary>
        private Bitmap _image;

        /// <summary>
        /// The blob min hight.
        /// </summary>
        public static int BlobMinHight = 50;

        /// <summary>
        /// The blob min width.
        /// </summary>
        public static int BlobMinWidth = 50;

        /// <summary>
        ///     Gets or sets the controls.
        /// </summary>
        /// <value>
        ///     The controls.
        /// </value>
        private List<Control> Controls { get; set; }

        /// <summary>
        ///     Initializes the specified origina image.
        /// </summary>
        /// <param name="originalImage">
        ///     The original image.
        /// </param>
        public void ProcessImage(Bitmap originalImage)
        {
            this.Reset();
            this.GenerateBlobs(originalImage);
        }

        /// <summary>
        ///     The detect.
        /// </summary>
        /// <returns>
        ///     The <see cref="Bitmap" />.
        /// </returns>
        public Bitmap GetImage()
        {
            if (this._image == null)
            {
                throw new MethodAccessException("Process Image before accessing the image output.");
            }

            Bitmap painted = this.PaintImage();

            return painted;
        }

        /// <summary>
        ///     Gets the shapes.
        /// </summary>
        /// <returns>
        ///     The list of Controls.
        /// </returns>
        public List<Control> GetShapes()
        {
            if (this._image == null)
            {
                throw new MethodAccessException("Process Image before accessing the image output.");
            }

            this.DetectControls();
            return Controls;
        }

        /// <summary>
        ///     Processes the blobs.
        /// </summary>
        private void DetectControls()
        {
            this._blobs = this._blobCounter.GetObjectsInformation();

            this.Controls = new List<Control>();

            for (int i = 0, n = this._blobs.Length; i < n; i++)
            {
                var edgePoints = _blobCounter.GetBlobsEdgePoints(this._blobs[i]);

                Control control = edgePoints.ToControl();

                Controls.Add(control);
            }
        }

        /// <summary>
        ///     Generates the blobs.
        /// </summary>
        /// <param name="originalImage">
        ///     The image.
        /// </param>
        public void GenerateBlobs(Bitmap originalImage)
        {
            this._image = AForge.Imaging.Image.Clone(originalImage, PixelFormat.Format24bppRgb);

            this._blobCounter.ProcessImage(this._image);
            this._blobCounter.MinHeight = BlobMinHight;
            this._blobCounter.MinWidth = BlobMinWidth;
            this._blobCounter.CoupledSizeFiltering = true;
            this._blobCounter.FilterBlobs = true;
            this._blobs = this._blobCounter.GetObjectsInformation();

            var grahamScan = new GrahamConvexHull();

            foreach (var blob in this._blobs)
            {
                this.ProcessBlob(blob, grahamScan);
            }
        }

        /// <summary>
        ///     Processes the BLOB.
        /// </summary>
        /// <param name="blob">
        ///     The BLOB.
        /// </param>
        /// <param name="grahamScan">
        ///     The graham scan.
        /// </param>
        private void ProcessBlob(Blob blob, GrahamConvexHull grahamScan)
        {
            List<IntPoint> leftEdge, topEdge, rightEdge, bottomEdge;

            // collect edge points
            this._blobCounter.GetBlobsLeftAndRightEdges(blob, out leftEdge, out rightEdge);
            this._blobCounter.GetBlobsTopAndBottomEdges(blob, out topEdge, out bottomEdge);

            this._leftEdges.Add(blob.ID, leftEdge);
            this._rightEdges.Add(blob.ID, rightEdge);
            this._topEdges.Add(blob.ID, topEdge);
            this._bottomEdges.Add(blob.ID, bottomEdge);

            // find convex hull
            var edgePoints = new List<IntPoint>();
            edgePoints.AddRange(leftEdge);
            edgePoints.AddRange(rightEdge);

            var hull = grahamScan.FindHull(edgePoints);
            this._hulls.Add(blob.ID, hull);

            // find quadrilateral
            List<IntPoint> quadrilateral = hull.Count < 4 ? new List<IntPoint>(hull) : PointsCloud.FindQuadrilateralCorners(hull);

            this._quadrilaterals.Add(blob.ID, quadrilateral);

            // shift all points for visualization
            IntPoint shift = new IntPoint(1, 1);

            PointsCloud.Shift(leftEdge, shift);
            PointsCloud.Shift(rightEdge, shift);
            PointsCloud.Shift(topEdge, shift);
            PointsCloud.Shift(bottomEdge, shift);
            PointsCloud.Shift(hull, shift);
            PointsCloud.Shift(quadrilateral, shift);
        }

        /// <summary>
        ///     Resets this instance.
        /// </summary>
        private void Reset()
        {
            this._leftEdges.Clear();
            this._rightEdges.Clear();
            this._topEdges.Clear();
            this._bottomEdges.Clear();
            this._hulls.Clear();
            this._quadrilaterals.Clear();
        }

        /// <summary>
        ///     The paint method of the originaImage.
        /// </summary>
        private Bitmap PaintImage()
        {
            Bitmap pained = (Bitmap)_image.Clone();
            var g = Graphics.FromImage(pained);

            var highlightPen = new Pen(Color.Red);
            var highlightPenBold = new Pen(Color.FromArgb(0, 255, 0), 3);

            // draw rectangle
            if (this._image != null)
            {
                foreach (var blob in this._blobs)
                {
                    var pen = highlightPen;

                    List<IntPoint> leftEdges, topEdges, rightEdges, bottomEdges;

                    this._leftEdges.TryGetValue(blob.ID, out leftEdges);
                    this._rightEdges.TryGetValue(blob.ID, out rightEdges);
                    this._bottomEdges.TryGetValue(blob.ID, out bottomEdges);
                    this._topEdges.TryGetValue(blob.ID, out topEdges);

                    List<IntPoint> edges = new List<IntPoint>();

                    if (topEdges != null) edges.AddRange(topEdges);
                    if (rightEdges != null) edges.AddRange(rightEdges);
                    if (bottomEdges != null) edges.AddRange(bottomEdges);
                    if (leftEdges != null) edges.AddRange(leftEdges);

                    g.DrawPolygon(highlightPenBold, (this._hulls[blob.ID]).ToPointsArray());
                    g.DrawPolygon(pen, (edges).ToPointsArray());

                }
            }

            return pained;
        }
    }
}