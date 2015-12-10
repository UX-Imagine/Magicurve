using System;
using System.Drawing;
using System.Drawing.Imaging;
using Uximagine.Magicurve.Image.Processing.Common;

namespace Uximagine.Magicurve.Image.Processing.Helpers
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Convolution helper.
    /// </summary>
    public static class ConvolutionFilterHelper
    {
        /// <summary>
        /// Conv3s the x3.
        /// </summary>
        /// <param name="b">
        /// The bitmap.
        /// </param>
        /// <param name="m">
        /// The matrix.
        /// </param>
        /// <returns>
        /// <c> true</c> if sucess. otherwise <c>false</c>.
        /// </returns>
        public static bool Conv3X3(Bitmap b, ConvolutionMatrix m)
        {
            // Avoid divide by zero errors
            if (0 == m.Factor)
                return false; Bitmap

            // GDI+ still lies to us - the return format is BGR, NOT RGB. 
            bSrc = (Bitmap)b.Clone();
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
                                ImageLockMode.ReadWrite,
                                PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height),
                               ImageLockMode.ReadWrite,
                               PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            int stride2 = stride * 2;

            IntPtr scan0 = bmData.Scan0;
            IntPtr srcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)scan0;
                byte* pSrc = (byte*)(void*)srcScan0;
                int nOffset = stride - b.Width * 3;
                int nWidth = b.Width - 2;
                int nHeight = b.Height - 2;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWidth; ++x)
                    {
                        nPixel = ((((pSrc[2] * m.TopLeft) +
                            (pSrc[5] * m.TopMid) +
                            (pSrc[8] * m.TopRight) +
                            (pSrc[2 + stride] * m.MidLeft) +
                            (pSrc[5 + stride] * m.Pixel) +
                            (pSrc[8 + stride] * m.MidRight) +
                            (pSrc[2 + stride2] * m.BottomLeft) +
                            (pSrc[5 + stride2] * m.BottomMid) +
                            (pSrc[8 + stride2] * m.BottomRight))
                            / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[5 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[1] * m.TopLeft) +
                            (pSrc[4] * m.TopMid) +
                            (pSrc[7] * m.TopRight) +
                            (pSrc[1 + stride] * m.MidLeft) +
                            (pSrc[4 + stride] * m.Pixel) +
                            (pSrc[7 + stride] * m.MidRight) +
                            (pSrc[1 + stride2] * m.BottomLeft) +
                            (pSrc[4 + stride2] * m.BottomMid) +
                            (pSrc[7 + stride2] * m.BottomRight))
                            / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[4 + stride] = (byte)nPixel;

                        nPixel = ((((pSrc[0] * m.TopLeft) +
                                       (pSrc[3] * m.TopMid) +
                                       (pSrc[6] * m.TopRight) +
                                       (pSrc[0 + stride] * m.MidLeft) +
                                       (pSrc[3 + stride] * m.Pixel) +
                                       (pSrc[6 + stride] * m.MidRight) +
                                       (pSrc[0 + stride2] * m.BottomLeft) +
                                       (pSrc[3 + stride2] * m.BottomMid) +
                                       (pSrc[6 + stride2] * m.BottomRight))
                            / m.Factor) + m.Offset);

                        if (nPixel < 0) nPixel = 0;
                        if (nPixel > 255) nPixel = 255;
                        p[3 + stride] = (byte)nPixel;

                        p += 3;
                        pSrc += 3;
                    }

                    p += nOffset;
                    pSrc += nOffset;
                }
            }

            b.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);
            return true;
        }

        /// <summary>
        /// Convolution3s the by3.
        /// </summary>
        /// <param name="bMap">
        /// The image.
        /// </param>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        /// <returns>
        /// The filtered image.
        /// </returns>
        public static Bitmap Convolution3By3(this Bitmap bMap, ConvolutionMatrix matrix)
        {
            //Avoid divide by 0 error;
            if (0 == matrix.Factor)
            {
                throw new ArgumentException("Factor cannot be zero.");
            }

            Bitmap bSrc = (Bitmap)bMap.Clone();
            BitmapData bmData = bMap.LockBits(
                new Rectangle(0, 0, bMap.Width, bMap.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);
            BitmapData bmSrc = bSrc.LockBits(
                new Rectangle(0, 0, bSrc.Width, bSrc.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            int srcStride = bmSrc.Stride;

            IntPtr scan0 = bmData.Scan0;
            IntPtr srcScan0 = bmSrc.Scan0;

            unsafe
            {
                byte* ptr = (byte*)(void*)scan0;
                byte* ptrSrc = (byte*)(void*)srcScan0;
                int nOffset = stride - bMap.Width * 3;
                int nWdith = bMap.Width - 2;
                int nHeight = bMap.Height - 2;

                int nPixel;

                for (int y = 0; y < nHeight; ++y)
                {
                    for (int x = 0; x < nWdith; ++x)
                    {
                        nPixel =
                            (((
                            (ptrSrc[2] * matrix.TopLeft) +
                            (ptrSrc[5] * matrix.TopMid) +
                            (ptrSrc[8] * matrix.TopRight) +
                            (ptrSrc[2 + stride] * matrix.MidLeft) +
                            (ptrSrc[5 + stride] * matrix.Pixel) +
                            (ptrSrc[8 + stride] * matrix.MidRight) +
                            (ptrSrc[2 + srcStride] * matrix.BottomLeft) +
                            (ptrSrc[5 + srcStride] * matrix.BottomMid) +
                            (ptrSrc[8 + srcStride] * matrix.BottomRight)
                            ) / matrix.Factor) + matrix.Offset);

                        nPixel = Math.Max(nPixel, 0);
                        nPixel = Math.Min(255, nPixel);

                        ptr[5 + stride] = (byte)nPixel;

                        nPixel =
                           (((
                           (ptrSrc[1] * matrix.TopLeft) +
                           (ptrSrc[4] * matrix.TopMid) +
                           (ptrSrc[7] * matrix.TopRight) +
                           (ptrSrc[1 + stride] * matrix.MidLeft) +
                           (ptrSrc[4 + stride] * matrix.Pixel) +
                           (ptrSrc[7 + stride] * matrix.MidRight) +
                           (ptrSrc[1 + srcStride] * matrix.BottomLeft) +
                           (ptrSrc[4 + srcStride] * matrix.BottomMid) +
                           (ptrSrc[7 + srcStride] * matrix.BottomRight)
                           ) / matrix.Factor) + matrix.Offset);

                        nPixel = Math.Max(nPixel, 0);
                        nPixel = Math.Min(255, nPixel);

                        ptr[4 + stride] = (byte)nPixel;

                        nPixel =
                           (((
                           (ptrSrc[0] * matrix.TopLeft) +
                           (ptrSrc[3] * matrix.TopMid) +
                           (ptrSrc[6] * matrix.TopRight) +
                           (ptrSrc[0 + stride] * matrix.MidLeft) +
                           (ptrSrc[3 + stride] * matrix.Pixel) +
                           (ptrSrc[6 + stride] * matrix.MidRight) +
                           (ptrSrc[0 + srcStride] * matrix.BottomLeft) +
                           (ptrSrc[3 + srcStride] * matrix.BottomMid) +
                           (ptrSrc[6 + srcStride] * matrix.BottomRight)
                           ) / matrix.Factor) + matrix.Offset);

                        nPixel = Math.Max(nPixel, 0);
                        nPixel = Math.Min(255, nPixel);

                        ptr[3 + stride] = (byte)nPixel;

                        ptr += 3;
                        ptrSrc += 3;
                    }
                    ptr += nOffset;
                    ptrSrc += nOffset;
                }
            }

            bMap.UnlockBits(bmData);
            bSrc.UnlockBits(bmSrc);

            return bMap;
        }

        /// <summary>
        /// Smoothes the specified weight.
        /// </summary>
        /// <param name="bitmap">
        /// The bitmap.
        /// </param>
        /// <param name="weight">
        /// The weight.
        /// </param>
        /// <returns>
        /// The smoothed image.
        /// </returns>
        public static Bitmap Smooth(this Bitmap bitmap, int weight)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix();
            matrix.SetAll(1);
            matrix.Pixel = weight;
            matrix.Factor = weight + 8;

            return bitmap.Convolution3By3(matrix);
        }

        /// <summary>
        /// Gaussians the blur.
        /// </summary>
        /// <param name="bitmap">
        /// The bitmap.
        /// </param>
        /// <returns>
        /// The gaussian filtered image.
        /// </returns>
        public static Bitmap GaussianBlur(this Bitmap bitmap)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix()
            {
                TopLeft = 1,
                TopMid = 2,
                TopRight = 1,
                MidLeft = 2,
                Pixel = 4,
                MidRight = 2,
                BottomLeft = 1,
                BottomMid = 2,
                BottomRight = 1,
                Factor = 16
            };

            return bitmap.Convolution3By3(matrix);
        }

        /// <summary>
        /// Sharpens the specified bitmap.
        /// </summary>
        /// <param name="bitmap">
        /// The bitmap.
        /// </param>
        /// <returns>
        /// The sharpened image.
        /// </returns>
        public static Bitmap Sharpen(this Bitmap bitmap)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix()
            {
                TopLeft = 0,
                TopMid = -2,
                TopRight = 0,
                MidLeft = -2,
                Pixel = 11,
                MidRight = -2,
                BottomLeft = 0,
                BottomMid = -2,
                BottomRight = 0,
                Factor = 3
            };

            return bitmap.Convolution3By3(matrix);
        }

        /// <summary>
        /// Edges the detect.
        /// </summary>
        /// <param name="bitmap">
        /// The bitmap.
        /// </param>
        /// <returns>
        /// The edges.
        /// </returns>
        public static Bitmap EdgeDetect(this Bitmap bitmap)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix()
            {
                TopLeft = 1,
                TopMid = -1,
                TopRight = 1,
                MidLeft = 0,
                Pixel = 0,
                MidRight = 0,
                BottomLeft = -1,
                BottomMid = -1,
                BottomRight = -1,
                Factor = 1,
                Offset = 127
            };

            return bitmap.Convolution3By3(matrix);
        }

        /// <summary>
        /// Edges the detect.
        /// </summary>
        /// <param name="bitmap">
        /// The bitmap.
        /// </param>
        /// <returns>
        /// The edges.
        /// </returns>
        public static bool EdgeDetectV2(Bitmap bitmap)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix()
            {
                TopLeft = 1,
                TopMid = -1,
                TopRight = 1,
                MidLeft = 0,
                Pixel = 0,
                MidRight = 0,
                BottomLeft = -1,
                BottomMid = -1,
                BottomRight = -1,
                Factor = 1,
                Offset = 127
            };

            return Conv3X3(bitmap, matrix);
        }

        /// <summary>
        /// Emboses the specified bitmap.
        /// </summary>
        /// <param name="bitmap">
        /// The bitmap.
        /// </param>
        /// <returns>
        /// The embosed image.
        /// </returns>
        public static Bitmap Embose(this Bitmap bitmap)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix()
            {
                TopLeft = -1,
                TopMid = -0,
                TopRight = -1,
                MidLeft = 0,
                Pixel = 4,
                MidRight = 0,
                BottomLeft = -1,
                BottomMid = 0,
                BottomRight = -1,
                Factor = 1,
                Offset = 127
            };

            return bitmap.Convolution3By3(matrix);
        }

        /// <summary>
        /// Emboses the specified bitmap.
        /// </summary>
        /// <param name="bitmap">
        /// The bitmap.
        /// </param>
        /// <returns>
        /// The embosed image.
        /// </returns>
        public static Bitmap EmboseBothDirections(this Bitmap bitmap)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix()
            {
                TopLeft = 0,
                TopMid = -1,
                TopRight = 0,
                MidLeft = -1,
                Pixel = 4,
                MidRight = -1,
                BottomLeft = 0,
                BottomMid = -1,
                BottomRight = 0,
                Factor = 1,
                Offset = 127
            };

            return bitmap.Convolution3By3(matrix);
        }

        /// <summary>
        /// Emboses the specified bitmap.
        /// </summary>
        /// <param name="bitmap">
        /// The bitmap.
        /// </param>
        /// <returns>
        /// The embosed image.
        /// </returns>
        public static Bitmap EmboseAllDirections(this Bitmap bitmap)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix()
            {
                TopLeft = -1,
                TopMid = -1,
                TopRight = -1,
                MidLeft = -1,
                Pixel = 8,
                MidRight = -1,
                BottomLeft = -1,
                BottomMid = -1,
                BottomRight = -1,
                Factor = 1,
                Offset = 127
            };

            return bitmap.Convolution3By3(matrix);
        }

        /// <summary>
        /// Emboses the specified bitmap.
        /// </summary>
        /// <param name="bitmap">
        /// The bitmap.
        /// </param>
        /// <returns>
        /// The embosed image.
        /// </returns>
        public static Bitmap EmboseHorizontal(this Bitmap bitmap)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix()
            {
                TopLeft = 0,
                TopMid = 0,
                TopRight = 0,
                MidLeft = -1,
                Pixel = 2,
                MidRight = -1,
                BottomLeft = 0,
                BottomMid = 0,
                BottomRight = 0,
                Factor = 1,
                Offset = 127
            };

            return bitmap.Convolution3By3(matrix);
        }

        /// <summary>
        /// Emboses the specified bitmap.
        /// </summary>
        /// <param name="bitmap">
        /// The bitmap.
        /// </param>
        /// <returns>
        /// The embosed image.
        /// </returns>
        public static Bitmap EmboseVertical(this Bitmap bitmap)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix()
            {
                TopLeft = 0,
                TopMid = -1,
                TopRight = 0,
                MidLeft = 0,
                Pixel = 0,
                MidRight = 0,
                BottomLeft = 0,
                BottomMid = -1,
                BottomRight = 0,
                Factor = 1,
                Offset = 127
            };

            return bitmap.Convolution3By3(matrix);
        }

        /// <summary>
        /// Convolutions the filter.
        /// </summary>
        /// <param name="sourceBitmap">The source bitmap.</param>
        /// <param name="filterMatrix">The filter matrix.</param>
        /// <param name="factor">The factor.</param>
        /// <param name="bias">The bias.</param>
        /// <param name="grayscale">if set to <c>true</c> [gray scale].</param>
        /// <returns>
        /// The convoluted image.
        /// </returns>
        public static Bitmap ConvolutionFilter(this Bitmap sourceBitmap,
                                     double[,] filterMatrix,
                                          double factor = 1,
                                               int bias = 0,
                                     bool grayscale = true)
        {
            BitmapData sourceData =
                           sourceBitmap.LockBits(new Rectangle(0, 0,
                           sourceBitmap.Width, sourceBitmap.Height),
                                             ImageLockMode.ReadOnly,
                                        PixelFormat.Format32bppArgb);


            byte[] pixelBuffer = new byte[sourceData.Stride *
                                          sourceData.Height];


            byte[] resultBuffer = new byte[sourceData.Stride *
                                           sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0,
                                       pixelBuffer.Length);


            sourceBitmap.UnlockBits(sourceData);


            if (grayscale == true)
            {
                for (int k = 0; k < pixelBuffer.Length; k += 4)
                {
                    float rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;


                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }


            double blue = 0.0;
            double green = 0.0;
            double red = 0.0;


            int filterWidth = filterMatrix.GetLength(1);
            int filterHeight = filterMatrix.GetLength(0);


            int filterOffset = (filterWidth - 1) / 2;
            int calcOffset = 0;


            int byteOffset = 0;


            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blue = 0;
                    green = 0;
                    red = 0;


                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;


                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {


                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);


                            blue += (double)(pixelBuffer[calcOffset]) *
                                    filterMatrix[filterY + filterOffset,
                                                 filterX + filterOffset];


                            green += (double)(pixelBuffer[calcOffset + 1]) *
                                     filterMatrix[filterY + filterOffset,
                                                  filterX + filterOffset];


                            red += (double)(pixelBuffer[calcOffset + 2]) *
                                   filterMatrix[filterY + filterOffset,
                                                filterX + filterOffset];
                        }
                    }


                    blue = factor * blue + bias;
                    green = factor * green + bias;
                    red = factor * red + bias;


                    if (blue > 255)
                    { blue = 255; }
                    else if (blue < 0)
                    { blue = 0; }


                    if (green > 255)
                    { green = 255; }
                    else if (green < 0)
                    { green = 0; }


                    if (red > 255)
                    { red = 255; }
                    else if (red < 0)
                    { red = 0; }


                    resultBuffer[byteOffset] = (byte)(blue);
                    resultBuffer[byteOffset + 1] = (byte)(green);
                    resultBuffer[byteOffset + 2] = (byte)(red);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }


            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width,
                                            sourceBitmap.Height);


            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                                        ImageLockMode.WriteOnly,
                                    PixelFormat.Format32bppArgb);


            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                                       resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }

        /// <summary>
        /// Convolutions the filter.
        /// </summary>
        /// <param name="sourceBitmap">The source bitmap.</param>
        /// <param name="xFilterMatrix">The x filter matrix.</param>
        /// <param name="yFilterMatrix">The y filter matrix.</param>
        /// <param name="factor">The factor.</param>
        /// <param name="bias">The bias.</param>
        /// <param name="grayscale">if set to <c>true</c> [gray-scale].</param>
        /// <returns>
        /// The filtered image.
        /// </returns>
        public static Bitmap ConvolutionFilter(this Bitmap sourceBitmap,
                                        double[,] xFilterMatrix,
                                        double[,] yFilterMatrix,
                                              double factor = 1,
                                                   int bias = 0,
                                         bool grayscale = false)
        {
            BitmapData sourceData =
                           sourceBitmap.LockBits(new Rectangle(0, 0,
                           sourceBitmap.Width, sourceBitmap.Height),
                                             ImageLockMode.ReadOnly,
                                        PixelFormat.Format32bppArgb);


            byte[] pixelBuffer = new byte[sourceData.Stride *
                                          sourceData.Height];


            byte[] resultBuffer = new byte[sourceData.Stride *
                                           sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0,
                                       pixelBuffer.Length);


            sourceBitmap.UnlockBits(sourceData);


            if (grayscale == true)
            {
                float rgb = 0;


                for (int k = 0; k < pixelBuffer.Length; k += 4)
                {
                    rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;


                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }


            double blueX = 0.0;
            double greenX = 0.0;
            double redX = 0.0;


            double blueY = 0.0;
            double greenY = 0.0;
            double redY = 0.0;


            double blueTotal = 0.0;
            double greenTotal = 0.0;
            double redTotal = 0.0;


            int filterOffset = 1;
            int calcOffset = 0;


            int byteOffset = 0;


            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blueX = greenX = redX = 0;
                    blueY = greenY = redY = 0;


                    blueTotal = greenTotal = redTotal = 0.0;


                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;


                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);


                            blueX += (double)
                                      (pixelBuffer[calcOffset]) *
                                      xFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];


                            greenX += (double)
                                  (pixelBuffer[calcOffset + 1]) *
                                      xFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];


                            redX += (double)
                                  (pixelBuffer[calcOffset + 2]) *
                                      xFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];


                            blueY += (double)
                                      (pixelBuffer[calcOffset]) *
                                      yFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];


                            greenY += (double)
                                  (pixelBuffer[calcOffset + 1]) *
                                      yFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];


                            redY += (double)
                                  (pixelBuffer[calcOffset + 2]) *
                                      yFilterMatrix[filterY +
                                                    filterOffset,
                                                    filterX +
                                                    filterOffset];
                        }
                    }


                    blueTotal = Math.Sqrt((blueX * blueX) +
                                          (blueY * blueY));


                    greenTotal = Math.Sqrt((greenX * greenX) +
                                           (greenY * greenY));


                    redTotal = Math.Sqrt((redX * redX) +
                                         (redY * redY));


                    if (blueTotal > 255)
                    { blueTotal = 255; }
                    else if (blueTotal < 0)
                    { blueTotal = 0; }


                    if (greenTotal > 255)
                    { greenTotal = 255; }
                    else if (greenTotal < 0)
                    { greenTotal = 0; }


                    if (redTotal > 255)
                    { redTotal = 255; }
                    else if (redTotal < 0)
                    { redTotal = 0; }


                    resultBuffer[byteOffset] = (byte)(blueTotal);
                    resultBuffer[byteOffset + 1] = (byte)(greenTotal);
                    resultBuffer[byteOffset + 2] = (byte)(redTotal);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }


            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width,
                                             sourceBitmap.Height);


            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                                        ImageLockMode.WriteOnly,
                                    PixelFormat.Format32bppArgb);


            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                                       resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }
    }
}
