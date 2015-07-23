namespace Uximagine.Magicurve.Image.Processing.Detectors
{
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing.Helpers;
using Uximagine.Magicurve.Image.Processing.ShapeCheckers;

    /// <summary>
    /// The blob detector.
    /// </summary>
    public class BlobDetector : IBlobDetector
    {
        /// <summary>
        /// Gets or sets the blobs.
        /// </summary>
        /// <value>
        /// The blobs.
        /// </value>
        public Blob[] Blobs { get; set; }

        /// <summary>
        /// Detects the specified original image.
        /// </summary>
        /// <param name="bitmap">
        /// The original image.
        /// </param>
        /// <returns>
        /// The detected image.
        /// </returns>
        public List<Control> GetShapes(Bitmap originalImage)
        {
            
            // step 2 - locating objects
            BlobCounter blobCounter = this.GetBlobs(originalImage);           
            this.Blobs = blobCounter.GetObjectsInformation();     

            //// step 3 - check objects' type and highlight
            AdvancedShapeChecker shapeChecker = new UIShapeChecker();

            List<Control> controls = new List<Control>();

            for (int i = 0, n = this.Blobs.Length; i < n; i++)
            {
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(this.Blobs[i]);

                ControlType type = shapeChecker.GetControlType(edgePoints);

                Control control = new Control();
                control.Edges = edgePoints;
                control.X = shapeChecker.X;
                control.Y = shapeChecker.Y;
                control.Width = shapeChecker.Width;
                control.Height = shapeChecker.Height;
                controls.Add(control);
            }

            return controls;
        }

        public Bitmap Detect(Bitmap bitmap)
        {
            // step 2 - locating objects
            BlobCounter blobCounter = this.GetBlobs(bitmap);
            this.Blobs = blobCounter.GetObjectsInformation();
            AdvancedShapeChecker shapeChecker = new UIShapeChecker();
            Graphics g = Graphics.FromImage(bitmap);
            Pen yellowPen = new Pen(Color.Yellow, 2); //// circles
            Pen redPen = new Pen(Color.Red, 2);       //// quadrilateral
            Pen brownPen = new Pen(Color.Brown, 2);   //// quadrilateral with known sub-type
            Pen greenPen = new Pen(Color.Green, 2);   //// known triangle
            Pen bluePen = new Pen(Color.Blue, 2);     //// triangle

            for (int i = 0, n = this.Blobs.Length; i < n; i++)
            {
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(this.Blobs[i]);

                AForge.Point center;
                float radius;

                //// is circle ?
                if (shapeChecker.IsCircle(edgePoints, out center, out radius))
                {
                    g.DrawEllipse(yellowPen,
                        (float)(center.X - radius), (float)(center.Y - radius),
                        (float)(radius * 2), (float)(radius * 2));
                }
                else
                {
                    List<IntPoint> corners;

                    //// is triangle or quadrilateral
                    /*if (shapeChecker.IsConvexPolygon(edgePoints, out corners))
                    {*/
                    //// get sub-type
                    //// PolygonSubType subType = shapeChecker.CheckPolygonSubType(corners);
                    corners = shapeChecker.GetShapeCorners(edgePoints);

                    ControlType controlType = shapeChecker.GetControlType(edgePoints);

                    Pen pen;

                    if (controlType == ControlType.None)
                    {
                        pen = bluePen;
                    }
                    else if (controlType == ControlType.Button)
                    {
                        pen = greenPen;
                    }
                    else if (controlType == ControlType.ComboBox)
                    {
                        pen = yellowPen;
                    }
                    else
                    {
                        pen = redPen;
                    }

                    g.DrawPolygon(pen, DrawingHelper.ToPointsArray(corners));
                }
            }

            yellowPen.Dispose();
            redPen.Dispose();
            greenPen.Dispose();
            bluePen.Dispose();
            brownPen.Dispose();
            g.Dispose();

            return bitmap;
        }

        private BlobCounter GetBlobs(Bitmap bitmap)
        {
            // lock image
            BitmapData bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);


            // step 1 - turn background to black
            ColorFiltering colorFilter = new ColorFiltering();

            colorFilter.Red = new IntRange(0, 64);
            colorFilter.Green = new IntRange(0, 64);
            colorFilter.Blue = new IntRange(0, 64);
            colorFilter.FillOutsideRange = false;

            colorFilter.ApplyInPlace(bitmapData);

            // step 2 - locating objects
            BlobCounter blobCounter = new BlobCounter();

            blobCounter.FilterBlobs = true;
            blobCounter.MinHeight = 20;
            blobCounter.MinWidth = 20;

            blobCounter.ProcessImage(bitmapData);
            bitmap.UnlockBits(bitmapData);

            return blobCounter;

        }
    }
}
