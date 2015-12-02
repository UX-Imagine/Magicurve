using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Uximagine.Magicurve.Image.Processing.Helpers
{
    /// <summary>
    /// The resize helper.
    /// </summary>
    public static class ResizeHelper
    {
        /// <summary>
        /// Resizes the specified img.
        /// </summary>
        /// <param name="img">The img.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns> The resized Image.</returns>
        private static System.Drawing.Image Resize(System.Drawing.Image img, int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage((System.Drawing.Image)b))
            {
                g.DrawImage(img, 0, 0, width, height);
            }

            return b;
        }

        /// <summary>
        /// Crops the specified width.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>The croped image.</returns>
        public static System.Drawing.Image Crop(this System.Drawing.Image image, int width, int height)
        {
            int cropx = image.Width > width ? image.Width / 2 - width / 2 : 0;
            int cropy = image.Height > height ? image.Height / 2 - height / 2 : 0;
            width = image.Width > width ? width : image.Width;
            height = image.Height > height ? height : image.Height;

            Rectangle cropRect = new Rectangle(cropx, cropy, width, height);

            var target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(image, new Rectangle(0, 0, target.Width, target.Height), cropRect, GraphicsUnit.Pixel);
            }

            return target;
        }

        /// <summary>
        /// Fits to size.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns> The Resized image.</returns>
        public static System.Drawing.Image FitToSize(this System.Drawing.Image image, int width, int height)
        {
            var wratio = 1.0 * image.Width / width;
            var hratio = 1.0 * image.Height / height;

            int wresize;
            int hresize;

            if (wratio >= hratio && wratio > 1)
            {
                wresize = (int)Math.Round((double)image.Width / hratio);
                hresize = height;

                image = Resize(image, wresize, hresize);
                image = Crop(image, width, height);
            }
            else if (hratio >= wratio && hratio > 1)
            {
                hresize = (int)Math.Round((double)image.Height / wratio);
                wresize = width;

                image = Resize(image, wresize, hresize);
                image = Crop(image, width, height);
            }
            return image;

        }

        /// <summary>
        /// Resizes the specified source image.
        /// </summary>
        /// <param name="srcImage">The source image.</param>
        /// <param name="newWidth">The new width.</param>
        /// <param name="newHeight">The new height.</param>
        /// <returns> The resized image.</returns>
        public static Bitmap Resize(this Bitmap srcImage, int newWidth, int newHeight)
        {
            Bitmap newImage = new Bitmap(newWidth, newHeight);
            using (Graphics gr = Graphics.FromImage(newImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));
            }

            return newImage;
        }
    }
}
