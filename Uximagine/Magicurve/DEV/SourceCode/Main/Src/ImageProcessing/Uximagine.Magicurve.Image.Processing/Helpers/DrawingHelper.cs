using System;
using AForge;
using System.Collections.Generic;
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
        ///// <summary>
        ///// Convert list of AForge.NET's points to array of .NET points        
        ///// </summary>
        ///// <param name="points">
        ///// The points.
        ///// </param>
        ///// <returns>
        ///// System Drawing Points. 
        ///// </returns>
        //public static System.Drawing.Point[] ToPointsArray(List<IntPoint> points)
        //{
        //    System.Drawing.Point[] array = new System.Drawing.Point[points.Count];

        //    for (int i = 0, n = points.Count; i < n; i++)
        //    {
        //        array[i] = new System.Drawing.Point(points[i].X, points[i].Y);
        //    }

        //    return array;
        //}

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
        /// Edgeses to bitmap.
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

            for (int i = 0; i < points.Count; i++)
            {
                IntPoint newPoint = points[i] - minXy - new IntPoint(1,1);
                points[i] = newPoint;
                bitmap.SetPixel(newPoint.X, newPoint.Y, Color.White);
            }

            return bitmap;
        }
    }
}
