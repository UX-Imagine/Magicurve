// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HullBlobDetector.cs" company="Uximagine">
//   UX-imagine 2015.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Uximagine.Magicurve.Image.Processing.Detectors
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;

    using AForge;
    using AForge.Imaging;
    using AForge.Math.Geometry;

    using Uximagine.Magicurve.Image.Processing.Helpers;

    using Image = AForge.Imaging.Image;
    using Uximagine.Magicurve.Core.Shapes;

    /// <summary>
    /// The hull blob detector.
    /// </summary>
    public class HullBlobDetector : IBlobDetector
    {
        /// <summary>
        /// Blobs' highlight types enumeration
        /// </summary>
        public enum HightlightType
        {
            /// <summary>
            /// The convex hull.
            /// </summary>
            ConvexHull, 

            /// <summary>
            /// The left and right edges.
            /// </summary>
            LeftAndRightEdges, 

            /// <summary>
            /// The top and bottom edges.
            /// </summary>
            TopAndBottomEdges, 

            /// <summary>
            /// The quadrilateral.
            /// </summary>
            Quadrilateral
        }

        /// <summary>
        /// The blob counter.
        /// </summary>
        private readonly BlobCounter blobCounter = new BlobCounter();

        /// <summary>
        /// The bottom edges.
        /// </summary>
        private readonly Dictionary<int, List<IntPoint>> bottomEdges = new Dictionary<int, List<IntPoint>>();

        /// <summary>
        /// The hulls.
        /// </summary>
        private readonly Dictionary<int, List<IntPoint>> hulls = new Dictionary<int, List<IntPoint>>();

        /// <summary>
        /// The left edges.
        /// </summary>
        private readonly Dictionary<int, List<IntPoint>> leftEdges = new Dictionary<int, List<IntPoint>>();

        /// <summary>
        /// The quadrilaterals.
        /// </summary>
        private readonly Dictionary<int, List<IntPoint>> quadrilaterals = new Dictionary<int, List<IntPoint>>();

        /// <summary>
        /// The right edges.
        /// </summary>
        private readonly Dictionary<int, List<IntPoint>> rightEdges = new Dictionary<int, List<IntPoint>>();

        /// <summary>
        /// The top edges.
        /// </summary>
        private readonly Dictionary<int, List<IntPoint>> topEdges = new Dictionary<int, List<IntPoint>>();

        /// <summary>
        /// The blobs.
        /// </summary>
        private Blob[] blobs;

        /// <summary>
        /// The highlighting.
        /// </summary>
        private HightlightType highlighting = HightlightType.ConvexHull;

        /// <summary>
        /// The image.
        /// </summary>
        private Bitmap image;

        /// <summary>
        /// The image width.
        /// </summary>
        private int imageWidth;

        /// <summary>
        /// The image height.
        /// </summary>
        private int imageHeight;

        /// <summary>
        /// The selected blob id.
        /// </summary>
        private int selectedBlobID;

        /// <summary>
        /// The show rectangle around selection.
        /// </summary>
        private bool showRectangleAroundSelection;

        /// <summary>
        /// The biggest BLOB identifier.
        /// </summary>
        private int biggestBlobId;

        /// <summary>
        /// The detect.
        /// </summary>
        /// <param name="image">
        /// The original image.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/>.
        /// </returns>
        public Bitmap Detect(Bitmap image)
        {
            this.Reset();

            this.image = Image.Clone(image, PixelFormat.Format24bppRgb);

            this.imageWidth = this.image.Width;
            this.imageHeight = this.image.Height;

            this.blobCounter.ProcessImage(this.image);
            this.blobs = this.blobCounter.GetObjectsInformation();

            var grahamScan = new GrahamConvexHull();

            foreach (var blob in this.blobs)
            {
                this.ProcessBlob(blob, grahamScan);
            }

            this.PaintImage();

            return this.image;
        }

        /// <summary>
        /// Processes the BLOB.
        /// </summary>
        /// <param name="blob">
        /// The BLOB.
        /// </param>
        /// <param name="grahamScan">
        /// The graham scan.
        /// </param>
        private void ProcessBlob(Blob blob, GrahamConvexHull grahamScan)
        {
            var leftEdge = new List<IntPoint>();
            var rightEdge = new List<IntPoint>();
            var topEdge = new List<IntPoint>();
            var bottomEdge = new List<IntPoint>();

            // collect edge points
            this.blobCounter.GetBlobsLeftAndRightEdges(blob, out leftEdge, out rightEdge);
            this.blobCounter.GetBlobsTopAndBottomEdges(blob, out topEdge, out bottomEdge);

            this.leftEdges.Add(blob.ID, leftEdge);
            this.rightEdges.Add(blob.ID, rightEdge);
            this.topEdges.Add(blob.ID, topEdge);
            this.bottomEdges.Add(blob.ID, bottomEdge);

            // find convex hull
            var edgePoints = new List<IntPoint>();
            edgePoints.AddRange(leftEdge);
            edgePoints.AddRange(rightEdge);

            var hull = grahamScan.FindHull(edgePoints);
            this.hulls.Add(blob.ID, hull);

            List<IntPoint> quadrilateral = null;

            // find quadrilateral
            quadrilateral = hull.Count < 4 ? new List<IntPoint>(hull) : PointsCloud.FindQuadrilateralCorners(hull);

            this.quadrilaterals.Add(blob.ID, quadrilateral);

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
        /// Resets this instance.
        /// </summary>
        private void Reset()
        {
            this.leftEdges.Clear();
            this.rightEdges.Clear();
            this.topEdges.Clear();
            this.bottomEdges.Clear();
            this.hulls.Clear();
            this.quadrilaterals.Clear();
            this.selectedBlobID = 0;
        }

        /// <summary>
        /// The paint method of the image.
        /// </summary>
        private void PaintImage()
        {
            var g = Graphics.FromImage(this.image);

            var highlightPen = new Pen(Color.Red);
            var highlightPenBold = new Pen(Color.FromArgb(0, 255, 0), 3);
            var rectPen = new Pen(Color.Blue);

            // draw rectangle
            if (this.image != null)
            {
                foreach (var blob in this.blobs)
                {
                    var pen = (blob.ID == this.selectedBlobID) ? highlightPenBold : highlightPen;

                    if (this.showRectangleAroundSelection && (blob.ID == this.selectedBlobID))
                    {
                        g.DrawRectangle(rectPen, blob.Rectangle);
                    }

                    switch (this.highlighting)
                    {
                        case HightlightType.ConvexHull:
                            g.DrawPolygon(pen, (this.hulls[blob.ID]).ToPointsArray());
                            break;
                        case HightlightType.LeftAndRightEdges:
                            this.DrawEdge(g, pen, this.leftEdges[blob.ID]);
                            this.DrawEdge(g, pen, this.rightEdges[blob.ID]);
                            break;
                        case HightlightType.TopAndBottomEdges:
                            this.DrawEdge(g, pen, this.topEdges[blob.ID]);
                            this.DrawEdge(g, pen, this.bottomEdges[blob.ID]);
                            break;
                        case HightlightType.Quadrilateral:
                            g.DrawPolygon(pen, (this.quadrilaterals[blob.ID]).ToPointsArray());
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Draws the edge.
        /// </summary>
        /// <param name="g">
        /// The graphics.
        /// </param>
        /// <param name="pen">
        /// The pen.
        /// </param>
        /// <param name="edge">
        /// The edge.
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

        /// <summary>
        /// Gets the shapes.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>
        /// The list of controls.
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// Not implemented yet.
        /// </exception>
        public List<Control> GetShapes(Bitmap image)
        {
            throw new System.NotImplementedException();
        }
    }
}
