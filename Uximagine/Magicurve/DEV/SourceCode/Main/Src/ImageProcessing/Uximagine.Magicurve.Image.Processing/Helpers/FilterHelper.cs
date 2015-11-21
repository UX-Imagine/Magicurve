using System;
using System.Drawing;
using System.Drawing.Imaging;
using Uximagine.Magicurve.Image.Processing.Common;

namespace Uximagine.Magicurve.Image.Processing.Helpers
{
    /// <summary>
    /// The common filters are implemented here using c# pointers.
    /// </summary>
    public static class FilterHelper
    {
        /// <summary>
        /// Sets the color filter.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <param name="filterType">
        /// Type of the filter.
        /// </param>
        /// <returns> 
        /// The filtered image.
        /// </returns>
        public static Bitmap SetColorFilter(this Bitmap image, ColorFilterType filterType)
        {
            Bitmap bmap = (Bitmap)image.Clone();

            Color color;

            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    color = bmap.GetPixel(i, j);
                    int nPixelR = 0;
                    int nPixelG = 0;
                    int nPixelB = 0;

                    if (filterType == ColorFilterType.Red)
                    {
                        nPixelR = color.R;
                        nPixelG = color.G - 255;
                        nPixelB = color.B - 255;
                    }
                    else if (filterType == ColorFilterType.Green)
                    {
                        nPixelR = color.R - 255;
                        nPixelG = color.G;
                        nPixelB = color.B - 255;
                    }
                    else if (filterType == ColorFilterType.Blue)
                    {
                        nPixelR = color.R - 255;
                        nPixelG = color.G - 255;
                        nPixelB = color.B;
                    }

                    nPixelR = Math.Max(nPixelR, 0);
                    nPixelR = Math.Min(255, nPixelR);

                    nPixelG = Math.Max(nPixelG, 0);
                    nPixelG = Math.Min(255, nPixelG);

                    nPixelB = Math.Max(nPixelB, 0);
                    nPixelB = Math.Min(255, nPixelB);

                    bmap.SetPixel(i, j, Color.FromArgb(
                        (byte)nPixelR,
                        (byte)nPixelG,
                        (byte)nPixelB));
                }
            }

            return (Bitmap)bmap.Clone();
        }

        /// <summary>
        /// Sets the color filter v2.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <param name="filterType">
        /// Type of the filter.
        /// </param>
        /// <returns>
        /// The filtered image.
        /// </returns>
        public static Bitmap SetColorFilterV2(this Bitmap image, ColorFilterType filterType)
        {
            Bitmap bmap = (Bitmap)image.Clone();
            BitmapData data = bmap.LockBits(
                new Rectangle(0, 0, bmap.Width, bmap.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int stride = data.Stride;
            int nWdith = data.Width * 3;
            int nOffset = stride - nWdith;
            IntPtr scan0 = data.Scan0;

            unsafe
            {
                byte* ptr = (byte*)(void*)scan0;

                for (int y = 0; y < bmap.Height; ++y)
                {
                    for (int x = 0; x < bmap.Width; ++x)
                    {
                        int blue = ptr[0];
                        int green = ptr[1];
                        int red = ptr[2];

                        if (filterType == ColorFilterType.Blue)
                        {
                            green = green - 255;
                            red = red - 255;
                        }
                        else if (filterType == ColorFilterType.Green)
                        {
                            blue = blue - 255;
                            red = red - 255;
                        }
                        else if (filterType == ColorFilterType.Red)
                        {
                            blue = blue - 255;
                            green = green - 255;
                        }

                        blue = Math.Max(blue, 0);
                        blue = Math.Min(255, blue);

                        green = Math.Max(green, 0);
                        green = Math.Min(255, green);

                        red = Math.Max(red, 0);
                        red = Math.Min(255, red);

                        ptr[0] = (byte)blue;
                        ptr[1] = (byte)green;
                        ptr[2] = (byte)red;

                        ptr += 3;
                    }
                    ptr += nOffset;
                }
            }

            bmap.UnlockBits(data);
            return bmap;
        }

        /// <summary>
        /// Inverts the specified image.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The inverted image.
        /// </returns>
        public static Bitmap Invert(this Bitmap image)
        {
            Bitmap bmap = (Bitmap)image.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB. 
            BitmapData data = bmap.LockBits(new Rectangle(0, 0, bmap.Width, bmap.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int stride = data.Stride;   // width of single line.
            IntPtr scan0 = data.Scan0;  // Pointer to the data.

            unsafe
            {
                byte* ptr = (byte*)(void*)scan0;
                int nOffset = stride - bmap.Width * 3;    // calculate the padding.
                int nWidth = bmap.Width * 3;              // steps for width * BGR
                for (int y = 0; y < bmap.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        ptr[0] = (byte)(255 - ptr[0]);
                        ++ptr;
                    }

                    ptr += nOffset;     // skip the padding.
                }
            }

            bmap.UnlockBits(data);

            return bmap;
        }

        /// <summary>
        /// Grayscales the specified image.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The grayscaled image.
        /// </returns>
        public static Bitmap Grayscale(this Bitmap image)
        {
            Bitmap bmap = (Bitmap)image.Clone();

            BitmapData data = bmap.LockBits(
                new Rectangle(0, 0, bmap.Width, bmap.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int stride = data.Stride;
            int nOffset = stride - data.Width * 3;
            IntPtr scan0 = data.Scan0;

            unsafe
            {
                byte* ptr = (byte*)(void*)scan0;
                byte red, green, blue;

                for (int y = 0; y < bmap.Height; ++y)
                {
                    for (int x = 0; x < bmap.Width; ++x)
                    {
                        blue = ptr[0];
                        green = ptr[1];
                        red = ptr[1];

                        ptr[0] = ptr[1] = ptr[2] =
                            (byte)(.299 * red + .587 * green + .114 * blue);

                        ptr += 3;
                    }
                    ptr += nOffset;
                }
            }

            bmap.UnlockBits(data);

            return bmap;
        }

        /// <summary>
        /// Brightens the specified amount.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <param name="nBrightness">
        /// The brightness amount.
        /// </param>
        /// <returns>
        /// The result image.
        /// </returns>
        public static Bitmap Brighten(this Bitmap image, int nBrightness)
        {
            Bitmap bmap = (Bitmap)image.Clone();

            BitmapData data = bmap.LockBits(
                new Rectangle(0, 0, bmap.Width, bmap.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int nVal;
            int stride = data.Stride;
            int nOffset = stride - bmap.Width * 3;
            int nWidth = bmap.Width * 3;
            IntPtr scan0 = data.Scan0;

            unsafe
            {
                byte* ptr = (byte*)(void*)scan0;

                for (int y = 0; y < bmap.Height; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nVal = ptr[0] + nBrightness;

                        nVal = Math.Max(nVal, 0);
                        nVal = Math.Min(255, nVal);

                        ptr[0] = (byte)nVal;

                        ++ptr;
                    }

                    ptr += nOffset;
                }
            }

            bmap.UnlockBits(data);

            return bmap;
        }

        /// <summary>
        /// Contrasts the specified contrast.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <param name="contrast">
        /// The contrast.
        /// </param>
        /// <returns>
        /// The contrast result.
        /// </returns>
        public static Bitmap Contrast(this Bitmap image, int contrast)
        {
            Bitmap bmap = (Bitmap)image.Clone();

            BitmapData data = bmap.LockBits(
                new Rectangle(0, 0, bmap.Width, bmap.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int nWidth = bmap.Width * 3;
            int stride = data.Stride;
            int nOffset = stride - nWidth;
            IntPtr scan0 = data.Scan0;
            unsafe
            {
                byte* ptr = (byte*)(void*)scan0;
                int red;
                double pixel;

                for (int y = 0; y < bmap.Height; ++y)
                {
                    for (int x = 0; x < bmap.Width; ++x)
                    {
                        red = ptr[2];
                        pixel = red / 255.0;
                        pixel -= 0.5;
                        pixel *= contrast;
                        pixel += 0.5;
                        pixel *= 255;

                        pixel = Math.Max(pixel, 0);
                        pixel = Math.Min(255, pixel);

                        ptr[2] = (byte)pixel;

                        ptr += 3;
                    }

                    ptr += nOffset;
                }
            }

            bmap.UnlockBits(data);
            return bmap;
        }

        /// <summary>
        /// Gammas the specified image.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <param name="red"> 
        /// Red Value 
        /// </param>
        /// <param name="green">
        ///  Green Value 
        /// </param>
        /// <param name="blue">
        ///  Blue Value 
        /// </param>
        /// <returns>
        /// The gamma applied image.
        /// </returns>
        public static Bitmap Gamma(this Bitmap image, double red, double green, double blue)
        {
            if (red < .2 || red > 5) throw new ArgumentException("Invalid Red Value");
            if (green < .2 || green > 5) throw new ArgumentException("Invalid Green Value");
            if (blue < .2 || blue > 5) throw new ArgumentException("Invalid Blue Value");

            Bitmap bmap = (Bitmap)image.Clone();
            BitmapData data = bmap.LockBits(
                new Rectangle(0, 0, bmap.Width, bmap.Width),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int stride = data.Stride;
            int nWidth = data.Width * 3;
            int nOffset = stride - nWidth;
            IntPtr scan0 = data.Scan0;

            byte[] redGamma = new byte[256];
            byte[] greenGamma = new byte[256];
            byte[] blueGamma = new byte[256];

            for (int i = 0; i < 256; ++i)
            {
                redGamma[i] = (byte)Math.Min(
                    255,
                    (int)((255.0 * Math.Pow(i / 255.0, 1.0 / red)) + 0.5));
                greenGamma[i] = (byte)Math.Min(
                    255,
                    (int)((255.0 * Math.Pow(i / 255.0, 1.0 / green)) + 0.5));
                blueGamma[i] = (byte)Math.Min(
                    255,
                    (int)((255.0 * Math.Pow(i / 255.0, 1.0 / blue)) + 0.5));

            }

            unsafe
            {
                byte* ptr = (byte*)(void*)scan0;

                for (int y = 0; y < bmap.Height; ++y)
                {
                    for (int x = 0; x < bmap.Width; ++x)
                    {

                        ptr[0] = redGamma[ptr[0]];
                        ptr[1] = redGamma[ptr[1]];
                        ptr[2] = redGamma[ptr[2]];

                        ptr += 3;
                    }

                    ptr += nOffset;
                }
            }

            bmap.UnlockBits(data);

            return bmap;
        }
    }
}
