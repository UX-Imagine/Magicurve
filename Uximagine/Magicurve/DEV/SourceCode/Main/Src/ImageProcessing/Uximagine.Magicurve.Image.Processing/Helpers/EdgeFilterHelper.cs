﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using Uximagine.Magicurve.Image.Processing.Common;

namespace Uximagine.Magicurve.Image.Processing.Helpers
{
    /// <summary>
    /// Edge filter helper.
    /// </summary>
    public static class EdgeFilterHelper
    {
        /// <summary>
        /// Sobels the specified image.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The soble applied image.
        /// </returns>
        public static Bitmap ApplySobel(this Bitmap image)
        {
            ConvolutionMatrix martrix = new ConvolutionMatrix()
            {
                TopLeft = 1,
                TopMid = 2,
                TopRight = 1,
                MidLeft = 0,
                Pixel = 0,
                MidRight = 0,
                BottomLeft = -1,
                BottomMid = -2,
                BottomRight = -1,
                Factor = 1
            };

            return image.Convolution3By3(martrix);
        }

        /// <summary>
        /// Horizontals the edges.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The filtered edges.
        /// </returns>
        public static Bitmap HorizontalEdges(this Bitmap image)
        {
            ConvolutionMatrix martrix = new ConvolutionMatrix()
            {
                TopLeft = 0,
                TopMid = -9,
                TopRight = 0,
                MidLeft = 0,
                Pixel = 18,
                MidRight = 0,
                BottomLeft = 0,
                BottomMid = -9,
                BottomRight = 0,
                Factor = 9
            };

            return image.Convolution3By3(martrix);
        }

        /// <summary>
        /// Verticals the edges.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>
        /// The filte
        /// </returns>
        public static Bitmap VerticalEdges(this Bitmap image)
        {
            ConvolutionMatrix martrix = new ConvolutionMatrix()
            {
                TopLeft = 0,
                TopMid = 0,
                TopRight = 0,
                MidLeft = -9,
                Pixel = 18,
                MidRight = -9,
                BottomLeft =0,
                BottomMid = 0,
                BottomRight = 0,
                Factor = 9
            };

            return image.Convolution3By3(martrix);
        }

        /// <summary>
        /// Applies the prewitt.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The filtered image.
        /// </returns>
        public static Bitmap ApplyPrewitt(this Bitmap image)
        {
            ConvolutionMatrix martrix = new ConvolutionMatrix()
            {
                TopLeft = 1,
                TopMid = 1,
                TopRight = 1,
                MidLeft = 0,
                Pixel = 0,
                MidRight = 0,
                BottomLeft = -1,
                BottomMid = -1,
                BottomRight = -1,
                Factor = 1
            };

            return image.Convolution3By3(martrix);
        }

        /// <summary>
        /// Applies the kirsh.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The image.
        /// </returns>
        public static Bitmap ApplyKirsh(this Bitmap image)
        {
            ConvolutionMatrix martrix = new ConvolutionMatrix()
            {
                TopLeft = 5,
                TopMid = 5,
                TopRight = 5,
                MidLeft = -3,
                Pixel = -3,
                MidRight = -3,
                BottomLeft = -3,
                BottomMid = -3,
                BottomRight = -3,
                Factor = 1
            };

            return image.Convolution3By3(martrix);
        }

        /// <summary>
        /// Edges the detect homogenity.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <param name="threshold">
        /// The threshold.
        /// </param>
        /// <returns>
        /// The filtered image.
        /// </returns>
        public static Bitmap EdgeDetectHomogenity(this Bitmap image, byte threshold)
        {
            Bitmap bmSrc = (Bitmap)image.Clone();

            BitmapData bmData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            BitmapData bmData2 = bmSrc.LockBits(
                new Rectangle(0, 0, bmSrc.Width, bmSrc.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            IntPtr scan0 = bmData.Scan0;
            IntPtr srcScan0 = bmData2.Scan0;

            unsafe
            {
                byte* ptr = (byte*)(void*)scan0;
                byte* ptrSrc = (byte*)(void*)srcScan0;

                int nWidth = image.Width * 3;
                int nOffset = stride - nWidth;

                ptr += stride;
                ptrSrc += stride;

                for (int y = 1; y < image.Height - 1; ++y)
                {
                    ptr += 3;
                    ptrSrc += 3;

                    for (int x = 3; x < nWidth - 3; ++x)
                    {
                        int pixelMax, pixel;

                        pixelMax = Math.Abs(ptrSrc[0] - (ptrSrc + stride - 3)[0]);
                        pixel = Math.Abs(ptrSrc[0] - (ptrSrc + stride)[0]);
                        pixelMax = pixel > pixelMax ? pixel : pixelMax;

                        pixel = Math.Abs(ptrSrc[0] - (ptrSrc + stride + 3)[0]);
                        pixelMax = pixel > pixelMax ? pixel : pixelMax;

                        pixel = Math.Abs(ptrSrc[0] - (ptrSrc - stride)[0]);
                        pixelMax = pixel > pixelMax ? pixel : pixelMax;

                        pixel = Math.Abs(ptrSrc[0] - (ptrSrc + stride)[0]);
                        pixelMax = pixel > pixelMax ? pixel : pixelMax;

                        pixel = Math.Abs(ptrSrc[0] - (ptrSrc - stride - 3)[0]);
                        pixelMax = pixel > pixelMax ? pixel : pixelMax;

                        pixel = Math.Abs(ptrSrc[0] - (ptrSrc - stride)[0]);
                        pixelMax = pixel > pixelMax ? pixel : pixelMax;

                        pixel = Math.Abs(ptrSrc[0] - (ptrSrc - stride + 3)[0]);
                        pixelMax = pixel > pixelMax ? pixel : pixelMax;

                        if (pixelMax < threshold) pixelMax = 0;

                        ptr[0] = (byte)pixelMax;

                        ++ptr;
                        ++ptrSrc;
                    }

                    ptr += 3 + nOffset;
                    ptrSrc += 3 + nOffset;
                }
            }

            image.UnlockBits(bmData);
            bmSrc.UnlockBits(bmData2);

            return image;
        }

        /// <summary>
        /// Edges the detect difference.
        /// </summary>
        /// <param name="bmap">
        /// The bmap.
        /// </param>
        /// <param name="threshold">
        /// The threshold.
        /// </param>
        /// <returns>
        /// <c>true</c> if success. otherwise <c>false.</c>
        /// </returns>
        public static bool EdgeDetectDifference(this Bitmap bmap, byte threshold)
        {
            Bitmap bmap2 = (Bitmap) bmap.Clone();

            BitmapData bmData = bmap.LockBits(
                new Rectangle(0, 0, bmap.Width, bmap.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            BitmapData bmData2 = bmap2.LockBits(
                new Rectangle(0, 0, bmap2.Width, bmap2.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            IntPtr scan0 = bmData.Scan0;
            IntPtr scan02 = bmData2.Scan0;

            unsafe
            {
                byte* ptr = (byte*) (void*) scan0;
                byte* ptr2 = (byte*) (void*) scan02;

                int offset = stride - bmap.Width*3;
                int width = bmap.Width*3;

                ptr += stride;
                ptr2 += stride;

                for (int y = 1; y < bmap.Height - 1; ++y)
                {
                    ptr += 3;
                    ptr2 += 3;

                    for (int x = 3; x < width - 3; ++x)
                    {
                        int pixelMax = Math.Abs((ptr2 - stride + 3)[0] - (ptr2 + stride - 3)[0]);
                        int pixel = Math.Abs((ptr2 + stride + 3)[0] - (ptr2 - stride - 3)[0]);
                        pixelMax = pixel > pixelMax ? pixel : pixelMax;

                        pixel = Math.Abs((ptr2 - stride)[0] - (ptr2 + stride)[0]);
                        pixelMax = pixel > pixelMax ? pixel : pixelMax;

                        pixel = Math.Abs((ptr2 + 3)[0] - (ptr2 - 3)[0]);
                        pixelMax = pixel > pixelMax ? pixel : pixelMax;

                        if (pixelMax < threshold) pixelMax = 0;

                        ptr[0] = (byte) pixelMax;

                        ++ptr;
                        ++ptr2;
                    }

                    ptr += 3 + offset;
                    ptr += 3 + offset;
                }
            }

            bmap.UnlockBits(bmData);
            bmap2.UnlockBits(bmData2);

            return true;
        }

        /// <summary>
        /// Edges the enhancement.
        /// </summary>
        /// <param name="bmap">
        /// The image.
        /// </param>
        /// <param name="threshold">
        /// The threshold.
        /// </param>
        /// <returns>
        /// <c>true</c> if success. othewise <c>false</c>.
        /// </returns>
        public static bool EdgeEnhancement(this Bitmap bmap, byte threshold)
        {
            Bitmap bmap2 = (Bitmap) bmap.Clone();

            BitmapData bmData = bmap.LockBits(
                new Rectangle(0, 0, bmap.Width, bmap.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            BitmapData bmData2 = bmap2.LockBits(
                new Rectangle(0, 0, bmap2.Width, bmap2.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int stride = bmData.Stride;
            IntPtr scan0 = bmData.Scan0;
            IntPtr scan02 = bmData.Scan0;

            unsafe
            {
                byte* ptr = (byte*) (void*) scan0;
                byte* ptr2 = (byte*) (void*) scan02;

                int width = bmap.Width*3;
                int offset = stride - width;

                ptr += stride;
                ptr2 += stride;

                for (int y = 1; y < bmap.Height - 1; ++y)
                {
                    ptr += 3;
                    ptr2 += 3;

                    for (int x = 3; x < width - 3; ++x)
                    {
                        int pixelMax = 0, pixel = 0;

                        pixelMax = Math.Abs((ptr2 - stride + 3)[0] - (ptr2 + stride - 3)[0]);
                        pixel = Math.Abs((ptr2 + stride + 3)[0] - (ptr2 - stride - 3)[0]);
                        pixelMax = pixel > pixelMax ? pixel : pixelMax;

                        pixel = Math.Abs((ptr2 - stride)[0] - (ptr2 + stride)[0]);
                        pixelMax = pixel > pixelMax ? pixel : pixelMax;

                        pixel = Math.Abs((ptr2 + 3)[0] - (ptr2 - 3)[0]);
                        pixelMax = pixel > pixelMax ? pixel : pixelMax;

                        if (pixelMax > threshold && pixelMax > ptr[0])
                            ptr[0] = (byte) Math.Max(ptr[0], pixelMax);

                        ++ptr;
                        ++ptr2;

                    }

                    ptr += offset + 3;
                    ptr2 += offset + 3;
                }

            }

            bmap.UnlockBits(bmData);
            bmap2.UnlockBits(bmData2);

            return true;
        }
    }
}
