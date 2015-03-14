using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uximagine.Magicurve.Image.Processing.Helpers;

namespace Uximagine.Magicurve.Image.Processing.Detectors
{
    /// <summary>
    /// The blob detector.
    /// </summary>
    internal class BlobDetector : IDetector
    {
        /// <summary>
        /// Detects the specified original image.
        /// </summary>
        /// <param name="originalImage">The original image.</param>
        /// <returns>
        /// The detected image.
        /// </returns>
        public Bitmap Detect(Bitmap bitmap)
        {
            //Invert invertFilter = new Invert();

            //invertFilter.ApplyInPlace(bitmap);

            //Threshold threshold = new Threshold(38);


            //AForge.Imaging.Filters.ContrastCorrection Contrast = new ContrastCorrection(10);
            //AForge.Imaging.Filters.BrightnessCorrection Brightness = new BrightnessCorrection(-12);

            //Grayscale grayScaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);


            ///invertFilter.ApplyInPlace(bitmap);
            //Contrast.ApplyInPlace(bitmap);
            //Brightness.ApplyInPlace(bitmap);
            //bitmap = grayScaleFilter.Apply(bitmap);
            //threshold.ApplyInPlace(bitmap);

            /*
            // apply the filters
            AForge.Imaging.UnmanagedImage UnManagedImg = AForge.Imaging.UnmanagedImage.FromManagedImage((Bitmap)bitmap);

            invertFilter.ApplyInPlace(UnManagedImg);  
            Contrast.ApplyInPlace(UnManagedImg);
            Brightness.ApplyInPlace(UnManagedImg);
            UnManagedImg = grayScaleFilter.Apply(UnManagedImg);
            threshold.ApplyInPlace(UnManagedImg);

            bitmap = UnManagedImg.ToManagedImage();*/

            // lock image
            BitmapData bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);


            //step 1 - turn background to black
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
            Blob[] blobs = blobCounter.GetObjectsInformation();
            bitmap.UnlockBits(bitmapData);

            //// step 3 - check objects' type and highlight
            SimpleShapeChecker shapeChecker = new SimpleShapeChecker();

            Graphics g = Graphics.FromImage(bitmap);
            Pen yellowPen = new Pen(Color.Yellow, 2); //// circles
            Pen redPen = new Pen(Color.Red, 2);       //// quadrilateral
            Pen brownPen = new Pen(Color.Brown, 2);   //// quadrilateral with known sub-type
            Pen greenPen = new Pen(Color.Green, 2);   //// known triangle
            Pen bluePen = new Pen(Color.Blue, 2);     //// triangle

            for (int i = 0, n = blobs.Length; i < n; i++)
            {
                List<IntPoint> edgePoints = blobCounter.GetBlobsEdgePoints(blobs[i]);

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
                    if (shapeChecker.IsConvexPolygon(edgePoints, out corners))
                    {
                        //// get sub-type
                        PolygonSubType subType = shapeChecker.CheckPolygonSubType(corners);

                        Pen pen;

                        if (subType == PolygonSubType.Unknown)
                        {
                            pen = (corners.Count == 4) ? redPen : bluePen;
                        }
                        else
                        {
                            pen = (corners.Count == 4) ? brownPen : greenPen;
                        }

                        g.DrawPolygon(pen, DrawingHelper.ToPointsArray(corners));
                    }
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
    }
}
