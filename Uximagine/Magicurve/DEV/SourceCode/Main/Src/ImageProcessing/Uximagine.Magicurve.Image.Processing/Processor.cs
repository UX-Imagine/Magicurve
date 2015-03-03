﻿#region Imports
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Uximagine.Magicurve.Core.Models;
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
        public Bitmap ProcessImage(string path)
        {
            Bitmap bitmap = new Bitmap(path);

            Bitmap orginalImage = (Bitmap)bitmap.Clone();

            Invert invertFilter = new Invert();

            invertFilter.ApplyInPlace(bitmap);  
           
            Threshold threshold = new Threshold(38);
                      

            AForge.Imaging.Filters.ContrastCorrection Contrast = new ContrastCorrection(10);
            AForge.Imaging.Filters.BrightnessCorrection Brightness = new BrightnessCorrection(-12);

            Grayscale grayScaleFilter = new Grayscale( 0.2125, 0.7154, 0.0721 );


            ///invertFilter.ApplyInPlace(bitmap);
            //Contrast.ApplyInPlace(bitmap);
            //Brightness.ApplyInPlace(bitmap);
            bitmap = grayScaleFilter.Apply(bitmap);
            threshold.ApplyInPlace(bitmap);

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

            //colorFilter.ApplyInPlace(bitmapData);

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

            Graphics g = Graphics.FromImage(orginalImage);
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

                        g.DrawPolygon(pen, ToPointsArray(corners));
                    }
                }
            }

            yellowPen.Dispose();
            redPen.Dispose();
            greenPen.Dispose();
            bluePen.Dispose();
            brownPen.Dispose();
            g.Dispose();

            return orginalImage;
          
        }

        /// <summary>
        /// Convert list of AForge.NET's points to array of .NET points        
        /// </summary>
        /// <param name="points">
        /// The points.
        /// </param>
        /// <returns>
        /// System Drawing Points. 
        /// </returns>
        private static System.Drawing.Point[] ToPointsArray(List<IntPoint> points)
        {
            System.Drawing.Point[] array = new System.Drawing.Point[points.Count];

            for (int i = 0, n = points.Count; i < n; i++)
            {
                array[i] = new System.Drawing.Point(points[i].X, points[i].Y);
            }

            return array;
        }
    }
}
