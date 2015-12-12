namespace Uximagine.Magicurve.Image.Processing.Helpers
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    using AForge.Imaging.Filters;

    using Uximagine.Magicurve.Image.Processing.Common;

    /// <summary>
    ///     The common filters are implemented here using c# pointers.
    /// </summary>
    public static class FilterHelper
    {
        /// <summary>
        ///     Brightens the specified amount.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="brightness">The brightness amount.</param>
        /// <returns>
        ///     The result image.
        /// </returns>
        public static Bitmap Brighten(this Bitmap image, int brightness)
        {
            Bitmap bmap = (Bitmap)image.Clone();

            BitmapData data = bmap.LockBits(
                new Rectangle(0, 0, bmap.Width, bmap.Height), 
                ImageLockMode.ReadWrite, 
                PixelFormat.Format24bppRgb);

            int stride = data.Stride;
            int offset = stride - (bmap.Width * 3);
            int width = bmap.Width * 3;
            IntPtr scan0 = data.Scan0;

            unsafe
            {
                byte* ptr = (byte*)(void*)scan0;

                for (int y = 0; y < bmap.Height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        int val = ptr[0] + brightness;

                        val = Math.Max(val, 0);
                        val = Math.Min(255, val);

                        ptr[0] = (byte)val;

                        ++ptr;
                    }

                    ptr += offset;
                }
            }

            bmap.UnlockBits(data);

            return bmap;
        }

        /// <summary>
        ///     Contrasts the specified contrast.
        /// </summary>
        /// <param name="image">
        ///     The image.
        /// </param>
        /// <param name="contrast">
        ///     The contrast.
        /// </param>
        /// <returns>
        ///     The contrast result.
        /// </returns>
        public static Bitmap Contrast(this Bitmap image, int contrast)
        {
            Bitmap bmap = (Bitmap)image.Clone();

            BitmapData data = bmap.LockBits(
                new Rectangle(0, 0, bmap.Width, bmap.Height), 
                ImageLockMode.ReadWrite, 
                PixelFormat.Format24bppRgb);

            int width = bmap.Width * 3;
            int stride = data.Stride;
            int offset = stride - width;
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

                    ptr += offset;
                }
            }

            bmap.UnlockBits(data);
            return bmap;
        }

        /// <summary>
        ///     Gammas the specified image.
        /// </summary>
        /// <param name="image">
        ///     The image.
        /// </param>
        /// <param name="red">
        ///     Red Value
        /// </param>
        /// <param name="green">
        ///     Green Value
        /// </param>
        /// <param name="blue">
        ///     Blue Value
        /// </param>
        /// <returns>
        ///     The gamma applied image.
        /// </returns>
        public static Bitmap Gamma(this Bitmap image, double red, double green, double blue)
        {
            if (red < .2 || red > 5)
            {
                throw new ArgumentException("Invalid Red Value");
            }

            if (green < .2 || green > 5)
            {
                throw new ArgumentException("Invalid Green Value");
            }

            if (blue < .2 || blue > 5)
            {
                throw new ArgumentException("Invalid Blue Value");
            }

            Bitmap bmap = (Bitmap)image.Clone();
            BitmapData data = bmap.LockBits(
                new Rectangle(0, 0, bmap.Width, bmap.Width), 
                ImageLockMode.ReadWrite, 
                PixelFormat.Format24bppRgb);

            int stride = data.Stride;
            int width = data.Width * 3;
            int offset = stride - width;
            IntPtr scan0 = data.Scan0;

            byte[] redGamma = new byte[256];
            byte[] greenGamma = new byte[256];
            byte[] blueGamma = new byte[256];

            for (int i = 0; i < 256; ++i)
            {
                redGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow((i / 255.0), (1.0 / red))) + 0.5));
                greenGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow((i / 255.0), (1.0 / green))) + 0.5));
                blueGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow((i / 255.0), (1.0 / blue))) + 0.5));
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

                    ptr += offset;
                }
            }

            bmap.UnlockBits(data);

            return bmap;
        }

        /// <summary>
        ///     Gets the BLOB ready.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>
        ///     The filtered image.
        /// </returns>
        public static Bitmap GetBlobReady(this Bitmap image)
        {
            image = AForge.Imaging.Filters.Grayscale.CommonAlgorithms.BT709.Apply(image);

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            Median median = new Median();
            median.ApplyInPlace(image);

            Invert invert = new Invert();
            invert.ApplyInPlace(image);

            return image;
        }

        /// <summary>
        ///     Gets the BLOB ready.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>
        ///     The filtered image.
        /// </returns>
        public static Bitmap GetCleaned(this Bitmap image)
        {
            image = AForge.Imaging.Filters.Grayscale.CommonAlgorithms.BT709.Apply(image);

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            Median median = new Median();
            median.ApplyInPlace(image);

            return image;
        }

        /// <summary>
        ///     Gets the BLOB ready.
        /// </summary>
        /// <param name="sourceData">The source data.</param>
        /// <returns>
        ///     The filtered image.
        /// </returns>
        public static Bitmap GetCleaned(this BitmapData sourceData)
        {
            Bitmap image = AForge.Imaging.Filters.Grayscale.CommonAlgorithms.BT709.Apply(sourceData);

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            Median median = new Median();
            median.ApplyInPlace(image);

            return image;
        }

        /// <summary>
        ///     Gray-scales the specified image.
        /// </summary>
        /// <param name="image">
        ///     The image.
        /// </param>
        /// <returns>
        ///     The gray-scaled image.
        /// </returns>
        public static Bitmap Grayscale(this Bitmap image)
        {
            Bitmap bmap = (Bitmap)image.Clone();

            BitmapData data = bmap.LockBits(
                new Rectangle(0, 0, bmap.Width, bmap.Height), 
                ImageLockMode.ReadWrite, 
                PixelFormat.Format24bppRgb);

            int stride = data.Stride;
            int offset = stride - (data.Width * 3);
            IntPtr scan0 = data.Scan0;

            unsafe
            {
                byte* ptr = (byte*)(void*)scan0;

                for (int y = 0; y < bmap.Height; ++y)
                {
                    for (int x = 0; x < bmap.Width; ++x)
                    {
                        byte blue = ptr[0];
                        byte green = ptr[1];
                        byte red = ptr[2];

                        ptr[0] = ptr[1] = ptr[2] = (byte)((.299 * red) + (.587 * green) + (.114 * blue));

                        ptr += 3;
                    }

                    ptr += offset;
                }
            }

            bmap.UnlockBits(data);

            return bmap;
        }

        /// <summary>
        ///     Inverts the specified image.
        /// </summary>
        /// <param name="image">
        ///     The image.
        /// </param>
        /// <returns>
        ///     The inverted image.
        /// </returns>
        public static Bitmap Invert(this Bitmap image)
        {
            Bitmap bmap = (Bitmap)image.Clone();

            // GDI+ still lies to us - the return format is BGR, NOT RGB. 
            BitmapData data = bmap.LockBits(
                new Rectangle(0, 0, bmap.Width, bmap.Height), 
                ImageLockMode.ReadWrite, 
                PixelFormat.Format24bppRgb);

            int stride = data.Stride; // width of single line.
            IntPtr scan0 = data.Scan0; // Pointer to the data.

            unsafe
            {
                byte* ptr = (byte*)(void*)scan0;
                int offset = stride - (bmap.Width * 3); // calculate the padding.
                int width = bmap.Width * 3; // steps for width * BGR
                for (int y = 0; y < bmap.Height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        ptr[0] = (byte)(255 - ptr[0]);
                        ++ptr;
                    }

                    ptr += offset; // skip the padding.
                }
            }

            bmap.UnlockBits(data);

            return bmap;
        }

        /// <summary>
        ///     Sets the color filter.
        /// </summary>
        /// <param name="image">
        ///     The image.
        /// </param>
        /// <param name="filterType">
        ///     Type of the filter.
        /// </param>
        /// <returns>
        ///     The filtered image.
        /// </returns>
        public static Bitmap SetColorFilter(this Bitmap image, ColorFilterType filterType)
        {
            Bitmap bmap = (Bitmap)image.Clone();

            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    Color color = bmap.GetPixel(i, j);
                    int pixelR = 0;
                    int pixelG = 0;
                    int pixelB = 0;

                    if (filterType == ColorFilterType.Red)
                    {
                        pixelR = color.R;
                        pixelG = color.G - 255;
                        pixelB = color.B - 255;
                    }
                    else if (filterType == ColorFilterType.Green)
                    {
                        pixelR = color.R - 255;
                        pixelG = color.G;
                        pixelB = color.B - 255;
                    }
                    else if (filterType == ColorFilterType.Blue)
                    {
                        pixelR = color.R - 255;
                        pixelG = color.G - 255;
                        pixelB = color.B;
                    }

                    pixelR = Math.Max(pixelR, 0);
                    pixelR = Math.Min(255, pixelR);

                    pixelG = Math.Max(pixelG, 0);
                    pixelG = Math.Min(255, pixelG);

                    pixelB = Math.Max(pixelB, 0);
                    pixelB = Math.Min(255, pixelB);

                    bmap.SetPixel(i, j, Color.FromArgb((byte)pixelR, (byte)pixelG, (byte)pixelB));
                }
            }

            return (Bitmap)bmap.Clone();
        }

        /// <summary>
        ///     Sets the color filter v2.
        /// </summary>
        /// <param name="image">
        ///     The image.
        /// </param>
        /// <param name="filterType">
        ///     Type of the filter.
        /// </param>
        /// <returns>
        ///     The filtered image.
        /// </returns>
        public static Bitmap SetColorFilterV2(this Bitmap image, ColorFilterType filterType)
        {
            Bitmap bmap = (Bitmap)image.Clone();
            BitmapData data = bmap.LockBits(
                new Rectangle(0, 0, bmap.Width, bmap.Height), 
                ImageLockMode.ReadWrite, 
                PixelFormat.Format24bppRgb);

            int stride = data.Stride;
            int wdith = data.Width * 3;
            int offset = stride - wdith;
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

                    ptr += offset;
                }
            }

            bmap.UnlockBits(data);
            return bmap;
        }

        /// <summary>
        /// Binaries the specified image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="threshold">The threshold.</param>
        public static void ToBinary(this Bitmap image, byte threshold = 127)
        {
            BitmapData data = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);
            byte[] buffer = new byte[data.Stride * image.Height];
            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

            int channels = 4;

            for (int k = 0; k < buffer.Length; k += channels)
            {
                byte gray = (byte)((.299 * buffer[k + 2]) + (.587 * buffer[k + 1]) + (.114 * buffer[k]));
                byte binary = (byte)(gray > threshold ? 255 : 0);
                buffer[k] = binary;
                buffer[k + 1] = binary;
                buffer[k + 2] = binary;
                buffer[k + 3] = 255;
            }

            Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);

            image.UnlockBits(data);

        }

        /// <summary>
        /// Gets the lines count.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="count">The count.</param>
        public static void GetHorizontalLinesCount(this Bitmap image, out int count)
        {
            count = 0;
            bool foundLine = false;

            BitmapData data = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            byte[] buffer = new byte[data.Stride * image.Height];

            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

            const int Channels = 3;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = image.Width / 2; x < (image.Width / 2) + 1; x++)
                {
                    int byteOffset = (y * data.Stride) + (x * Channels);
                    //// Debug.WriteLine($"{buffer[byteOffset]} {x} {y}");
                    if (buffer[byteOffset] == 255)
                    {
                        foundLine = true;
                    }
                    else
                    {
                        if (foundLine)
                        {
                            count++;
                            foundLine = false;
                        }
                    }
                }
            }

            if (foundLine)
            {
                count++;
            }

            //// Finally
            Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);
            image.UnlockBits(data);
        }

        /// <summary>
        /// Gets the lines count.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="count">The count.</param>
        public static void GetVerticalLinesCount(this Bitmap image, out int count)
        {
            count = 0;
            bool foundLine = false;

            BitmapData data = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            byte[] buffer = new byte[data.Stride * image.Height];

            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

            const int Channels = 3;

            for (int y = image.Height / 2; y < (image.Height / 2) + 1; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int byteOffset = (y * data.Stride) + (x * Channels);
                    //// Debug.WriteLine($"{buffer[byteOffset]} {x} {y}");
                    if (buffer[byteOffset] == 255)
                    {
                        foundLine = true;
                    }
                    else
                    {
                        if (foundLine)
                        {
                            count++;
                            foundLine = false;
                        }
                    }
                }

                if (foundLine)
                {
                    count++;
                }
            }

            //// Finally
            Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);
            image.UnlockBits(data);
        }

        /// <summary>
        /// Gets the lines count.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="count">The count.</param>
        public static void GetHorizontalLinesCountV2(this Bitmap image, out int count)
        {
            count = 0;
            int lineStart = -1;
            bool foundLine = false;
            int lineLenght = 0;
            int longestLineLenth = 0;

            BitmapData data = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            byte[] buffer = new byte[data.Stride * image.Height];

            Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

            const int Channels = 3;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = image.Width / 2; x < (image.Width / 2) + 1; x++)
                {
                    int byteOffset = (y * data.Stride) + (x * Channels);

                    if (buffer[byteOffset] == 255)
                    {
                        if (!foundLine)
                        {
                            lineStart = y;
                        }

                        foundLine = true;
                    }
                    else
                    {
                        if (foundLine)
                        {
                            int middleOfLine = (y - lineStart) / 2;
                            for (int i = middleOfLine; i < middleOfLine + 1; i++)
                            {
                                for (int j = 0; j < image.Width; j += Channels)
                                {
                                    byteOffset = (i * data.Stride) + (j * Channels);
                                    int byteOffsetPrevious = ((i - 1) * data.Stride) + (j * Channels);
                                    int byteOffsetNext = ((i - 1) * data.Stride) + (j * Channels);
                                    if (buffer[byteOffset] == 255 || buffer[byteOffsetPrevious] == 255 || buffer[byteOffsetNext] == 255)
                                    {
                                        lineLenght++;
                                    }
                                    else
                                    {
                                        if (longestLineLenth < lineLenght)
                                        {
                                            longestLineLenth = lineLenght;
                                        }

                                        lineLenght = 0;
                                    }
                                }
                            }

                            if (longestLineLenth > 50)
                            {
                                count++;
                            }

                            foundLine = false;
                        }
                    }
                }
            }

            if (foundLine)
            {
                count++;
            }

            //// Finally
            Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);
            image.UnlockBits(data);
        }

    }
}