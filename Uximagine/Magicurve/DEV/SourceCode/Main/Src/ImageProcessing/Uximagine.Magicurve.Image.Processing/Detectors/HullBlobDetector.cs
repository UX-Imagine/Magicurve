// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HullBlobDetector.cs" company="Uximagine">
//   UX-imagine 2015.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
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
        ///     Blobs' highlight types enumeration
        /// </summary>
        public enum HightlightType
        {
            /// <summary>
            ///     The convex hull.
            /// </summary>
            ConvexHull,

            /// <summary>
            ///     The left and right edges.
            /// </summary>
            LeftAndRightEdges,

            /// <summary>
            ///     The top and bottom edges.
            /// </summary>
            TopAndBottomEdges,

            /// <summary>
            ///     The quadrilateral.
            /// </summary>
            Quadrilateral
        }

        /// <summary>
        ///     The highlighting.
        /// </summary>
        private const HightlightType Highlighting = HightlightType.ConvexHull;

        /// <summary>
        ///     The show rectangle around selection.
        /// </summary>
        private const bool ShowRectangleAroundSelection = false;

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
        ///     The selected blob id.
        /// </summary>
        private int _selectedBlobId;

        /// <summary>
        /// The blob min hight.
        /// </summary>
        private const int BLOB_MIN_HIGHT = 25;

        /// <summary>
        /// The blob min width.
        /// </summary>
        private const int BLOB_MIN_WIDTH = 25;

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

            this.PaintImage();

            return this._image;
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

            //// step 3 - check objects' type and highlight
            var shapeChecker = ProcessingFactory.GetShapeChecker();

            this.Controls = new List<Control>();

            for (int i = 0, n = this._blobs.Length; i < n; i++)
            {
                var edgePoints = _blobCounter.GetBlobsEdgePoints(this._blobs[i]);

                var type = shapeChecker.GetControlType(edgePoints);

                var control = new Control
                {
                    Type = type,
                    X = shapeChecker.X,
                    Y = shapeChecker.Y,
                    Width = shapeChecker.Width,
                    Height = shapeChecker.Height,
                    EdgePoints = edgePoints
                };

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
            this._blobCounter.MinHeight = BLOB_MIN_HIGHT;
            this._blobCounter.MinWidth = BLOB_MIN_WIDTH;
            this._blobCounter.CoupledSizeFiltering = true;
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
            var leftEdge = new List<IntPoint>();
            var rightEdge = new List<IntPoint>();
            var topEdge = new List<IntPoint>();
            var bottomEdge = new List<IntPoint>();

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

            List<IntPoint> quadrilateral = null;

            // find quadrilateral
            quadrilateral = hull.Count < 4 ? new List<IntPoint>(hull) : PointsCloud.FindQuadrilateralCorners(hull);

            this._quadrilaterals.Add(blob.ID, quadrilateral);

            // shift all points for visualization
            var shift = new IntPoint(1, 1);

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
            this._selectedBlobId = 0;
        }

        /// <summary>
        ///     The paint method of the originaImage.
        /// </summary>
        private void PaintImage()
        {
            var g = Graphics.FromImage(this._image);

            var highlightPen = new Pen(Color.Red);
            var highlightPenBold = new Pen(Color.FromArgb(0, 255, 0), 3);
            // var rectPen = new Pen(Color.Blue);

            // draw rectangle
            if (this._image != null)
            {
                foreach (var blob in this._blobs)
                {
                    var pen = highlightPen;
                    var leftEdges = new List<IntPoint>();
                    var topEdges = new List<IntPoint>();
                    var rightEdges = new List<IntPoint>();
                    var bottomEdges = new List<IntPoint>();

                    this._leftEdges.TryGetValue(blob.ID, out leftEdges);
                    this._rightEdges.TryGetValue(blob.ID, out rightEdges);
                    this._bottomEdges.TryGetValue(blob.ID, out bottomEdges);
                    this._topEdges.TryGetValue(blob.ID, out topEdges);

                    var edges = new List<IntPoint>();

                    edges.AddRange(topEdges);
                    edges.AddRange(rightEdges);
                    edges.AddRange(bottomEdges);
                    edges.AddRange(leftEdges);

                    /*
                                        if (ShowRectangleAroundSelection && (blob.ID == this._selectedBlobId))
                                        {
                                            g.DrawRectangle(rectPen, blob.Rectangle);
                                        }
                    */

                    switch (Highlighting)
                    {
                        case HightlightType.ConvexHull:
                            g.DrawPolygon(highlightPenBold, (this._hulls[blob.ID]).ToPointsArray());
                            g.DrawPolygon(pen, (edges).ToPointsArray());
                            break;
/*
                        case HightlightType.LeftAndRightEdges:
                            this.DrawEdge(g, pen, this._leftEdges[blob.ID]);
                            this.DrawEdge(g, pen, this._rightEdges[blob.ID]);
                            break;
                        case HightlightType.TopAndBottomEdges:
                            this.DrawEdge(g, pen, this._topEdges[blob.ID]);
                            this.DrawEdge(g, pen, this._bottomEdges[blob.ID]);
                            break;
                        case HightlightType.Quadrilateral:
                            g.DrawPolygon(pen, (this._quadrilaterals[blob.ID]).ToPointsArray());
                            break;
*/
                    }
                }
            }
        }

        /// <summary>
        ///     Draws the edge.
        /// </summary>
        /// <param name="g">
        ///     The graphics.
        /// </param>
        /// <param name="pen">
        ///     The pen.
        /// </param>
        /// <param name="edge">
        ///     The edge.
        /// </param>
        private void DrawEdge(Graphics g, Pen pen, List<IntPoint> edge)
        {
            var points = edge.ToPointsArray();

            if (points.Length > 1)
            {
                g.DrawLines(pen, points);
            }
            else
            {
                g.DrawLine(pen, points[0], points[0]);
            }
        }
    }
}