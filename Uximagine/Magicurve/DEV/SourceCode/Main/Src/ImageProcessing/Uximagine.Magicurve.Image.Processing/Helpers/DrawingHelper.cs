using AForge;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Uximagine.Magicurve.Image.Processing.Helpers
{
    /// <summary>
    /// Helper for drawing images.
    /// </summary>
    internal static class DrawingHelper
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
        public static System.Drawing.Point[] ToPointsArray(List<IntPoint> points)
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
        /// <param name="image">The image.</param>
        /// <param name="format">The format.</param>
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
    }
}
