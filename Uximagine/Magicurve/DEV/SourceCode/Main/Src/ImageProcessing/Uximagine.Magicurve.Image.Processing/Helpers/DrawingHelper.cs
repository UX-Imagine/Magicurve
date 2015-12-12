using System;
using AForge;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Math.Geometry;

namespace Uximagine.Magicurve.Image.Processing.Helpers
{
    /// <summary>
    /// Helper for drawing images.
    /// </summary>
    public static class DrawingHelper
    {
        /// <summary>
        /// Convert list of AForge.NET's points to array of .NET points        
        /// </summary>
        /// <param name="points">
        /// The points.
        /// </param>
        /// <returns>
        /// System Drawing Points. 
        /// </returns>
        public static System.Drawing.Point[] ToPointsArray(this List<IntPoint> points)
        {
            System.Drawing.Point[] array = new System.Drawing.Point[points.Count];

            for (int i = 0, n = points.Count; i < n; i++)
            {
                array[i] = new System.Drawing.Point(points[i].X, points[i].Y);
            }

            return array;
        }

        /// <summary>
        /// Converts to format.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <returns>
        /// The converted image.
        /// </returns>
        public static Bitmap ConvertToFormat(this Bitmap image, PixelFormat format)
        {
            Bitmap copy = new Bitmap(image.Width, image.Height, format);
            using (Graphics gr = Graphics.FromImage(copy))
            {
                gr.DrawImage(image, new Rectangle(0, 0, copy.Width, copy.Height));
            }

            return copy;
        }

        /// <summary>
        /// Edges to bitmap.
        /// </summary>
        /// <param name="points">
        /// The points.
        /// </param>
        /// <returns>
        /// The bitmap.
        /// </returns>
        public static Bitmap ConvertToBitmap(this List<IntPoint> points)
        {
            IntPoint minXy;
            IntPoint maxXy;

            PointsCloud.GetBoundingRectangle(points, out minXy, out maxXy);

            int width = Math.Abs(maxXy.X - minXy.X);
            int height = Math.Abs(maxXy.Y - minXy.Y);

            Bitmap bitmap = new Bitmap(width, height);

            for (int i = 0; i < points.Count; ++i)
            {
                Debug.WriteLine("min x,y :{0}, {1}", minXy.X, minXy.Y);

                IntPoint newPoint = points[i] - minXy;

                // Debug.WriteLine("new point {0},{1} oldX:{2}, OldY:{3}", newPoint.X, newPoint.Y, points[i].X, points[i].Y);
                points[i] = newPoint;

                try
                {
                    bitmap.SetPixel(newPoint.X, newPoint.Y, Color.White);
                }
                catch (Exception)
                {
                    //// Debug.WriteLine(exception.Message);
                    Debug.WriteLine("Illegal point {0},{1} width:{2}, height:{3}", newPoint.X, newPoint.Y, width, height);
                }
            }

            return bitmap;
        }

        /// <summary>
        /// Crops the specified edge points.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="edgePoints">The edge points.</param>
        /// <returns>
        /// The cropped image.
        /// </returns>
        public static Bitmap Crop(this Bitmap src, List<IntPoint> edgePoints)
        {
            return src.Crop(edgePoints, 0);
        }

        /// <summary>
        /// Crops the specified edge points.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="edgePoints">The edge points.</param>
        /// <param name="frame">The frame.</param>
        /// <returns>
        /// The cropped image.
        /// </returns>
        public static Bitmap Crop(this Bitmap src, List<IntPoint> edgePoints, int frame)
        {
            IntPoint minXy;
            IntPoint maxXy;

            PointsCloud.GetBoundingRectangle(edgePoints, out minXy, out maxXy);

            int width = Math.Abs(maxXy.X - minXy.X);
            int height = Math.Abs(maxXy.Y - minXy.Y);
            int minX = minXy.X - frame >= 0 ? minXy.X - frame : minXy.X;
            int minY = minXy.Y - frame >= 0 ? minXy.Y - frame : minXy.Y;
            int newWidth = width + frame <= src.Width ? width + frame : width;
            int newHeight = height + frame <= src.Height ? height + frame : height;

            Rectangle cropRect = new Rectangle(minX, minY, newWidth, newHeight);

            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }

            return target;
        }

        /// <summary>
        /// Extract vector from the specified edge points.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="edgePoints">The edge points.</param>
        /// <param name="frame">The frame.</param>
        /// <param name="sampleSize">Size of the sample.</param>
        /// <returns>
        /// The image vector.
        /// </returns>
        public static Bitmap Vectorize(this Bitmap image, List<IntPoint> edgePoints, int frame, int sampleSize)
        {
            Bitmap cropped = image.Crop(edgePoints, frame);

            cropped.ToBinary();

            cropped = cropped.Resize(sampleSize, sampleSize);

            //// cropped.Save("E:/Data/identified/cropped" + ".jpg");

            return cropped;
        }

        /// <summary>
        /// Extract vector from the specified edge points.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="edgePoints">The edge points.</param>
        /// <param name="frame">The frame.</param>
        /// <returns>
        /// The image vector.
        /// </returns>
        public static Bitmap Vectorize(this Bitmap image, List<IntPoint> edgePoints, int frame)
        {
            return image.Vectorize(edgePoints, frame, 32);
        }

        /// <summary>
        /// Extract vector from the specified edge points.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="edgePoints">The edge points.</param>
        /// <returns>
        /// The image vector.
        /// </returns>
        public static Bitmap Vectorize(this Bitmap image, List<IntPoint> edgePoints)
        {
            return image.Vectorize(edgePoints, 0);
        }

        /// <summary>
        /// Converts to bitmap v2.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns>
        /// The bitmap with those points included.
        /// </returns>
        public static Bitmap ConvertToBitmapV2(this List<IntPoint> points)
        {
            IntPoint minXy;
            IntPoint maxXy;

            PointsCloud.GetBoundingRectangle(points, out minXy, out maxXy);

            int width = Math.Abs(maxXy.X - minXy.X);
            int height = Math.Abs(maxXy.Y - minXy.Y);

            Bitmap bitmap = new Bitmap(width, height);
            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            for (int i = 0; i < points.Count; ++i)
            {
                IntPoint newPoint = points[i] - minXy;
                points[i] = newPoint;
            }

            bitmap.UnlockBits(data);
            return bitmap;
        }

    }
}
