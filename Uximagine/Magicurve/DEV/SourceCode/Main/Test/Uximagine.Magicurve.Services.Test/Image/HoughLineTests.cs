namespace Uximagine.Magicurve.Services.Test.Image
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;

    using Accord.Extensions.Imaging;

    using AForge;
    using AForge.Imaging;
    using AForge.Imaging.Filters;

    using DotImaging;

    using NUnit.Framework;
    using Should;
    using Uximagine.Magicurve.Image.Processing.Helpers;

    /// <summary>
    /// The hough line tests.
    /// </summary>
    [TestFixture]
    public class HoughLineTests
    {
        /// <summary>
        /// Tests the hough lines.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="horizontal">The horizontal count.</param>
        /// <param name="vertical">The vertical count.</param>
        [TestCase(@"D:\Data\test\inputs\label\text_12.jpg", 3, 2)]
        public void TestHoughLines(string fileName, int horizontal, int vertical)
        {
            using (Bitmap image = new Bitmap(fileName))
            {
                using (Bitmap blobs = image.GetCleaned())
                {
                    HoughLineTransformation hlt = new HoughLineTransformation();
                    hlt.ProcessImage(blobs);

                    int horizontalCount = 0;
                    int verticalCount = 0;
                    HoughLine[] lines = hlt.GetLinesByRelativeIntensity(1);
                    lines.Count().ShouldEqual(horizontalCount + verticalCount);

                    foreach (HoughLine line in hlt.GetLinesByRelativeIntensity(1))
                    {
                        if (line.Theta < 5 && line.Theta > -5)
                        {
                            horizontalCount++;
                        }
                        else if (line.Theta < 85 && line.Theta > 95)
                        {
                            verticalCount++;
                        }
                    }

                    horizontalCount.ShouldEqual(horizontal);
                    verticalCount.ShouldEqual(vertical);
                }
            }
        }

        /// <summary>
        /// Tests the draw.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        [TestCase(@"D:\Data\test\inputs\label/text_12.jpg")]
        public void TestDraw(string fileName)
        {
            Bitmap sourceImage = new Bitmap(fileName);

            sourceImage = Grayscale.CommonAlgorithms.BT709.Apply(sourceImage);
            
            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(sourceImage);

            Median median = new Median();
            median.ApplyInPlace(sourceImage);

            HoughLineTransformation lineTransform = new HoughLineTransformation();

            //// apply Hough line transform
            lineTransform.ProcessImage(sourceImage);

            BitmapData sourceData = sourceImage.LockBits(
               new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
               ImageLockMode.ReadWrite,
               sourceImage.PixelFormat);
            
            Bitmap houghLineImage = lineTransform.ToBitmap();

            //// get lines using relative intensity
            HoughLine[] lines = lineTransform.GetLinesByRelativeIntensity(0.5);

            foreach (HoughLine line in lines)
            {
                // get line's radius and theta values
                int r = line.Radius;
                double t = line.Theta;

                // check if line is in lower part of the image
                if (r < 0)
                {
                    t += 180;
                    r = -r;
                }

                // convert degrees to radians
                t = (t / 180) * Math.PI;

                // get image centers (all coordinate are measured relative
                // to center)
                int w2 = sourceImage.Width / 2;
                int h2 = sourceImage.Height / 2;

                double x0, x1, y0, y1;

                if (line.Theta != 0)
                {
                    // none-vertical line
                    x0 = -w2; // most left point
                    x1 = w2; // most right point

                    // calculate corresponding y values
                    y0 = (-Math.Cos(t) * x0 + r) / Math.Sin(t);
                    y1 = (-Math.Cos(t) * x1 + r) / Math.Sin(t);
                }
                else
                {
                    // vertical line
                    x0 = line.Radius;
                    x1 = line.Radius;

                    y0 = h2;
                    y1 = -h2;
                }

                // draw line on the image
                Drawing.Line(
                    sourceData,
                    new IntPoint((int)x0 + w2, h2 - (int)y0),
                    new IntPoint((int)x1 + w2, h2 - (int)y1),
                    Color.Red);
                
            }

            sourceImage.UnlockBits(sourceData);

            sourceImage.Save(@"D:/Data/test/outputs/hough_" + fileName.Split('/').Last());
        }

        /// <summary>
        /// Tests the extensions.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        [TestCase(@"D:\Data\test\inputs\label/text_12.jpg")]
        public void TestExtentions(string fileName)
        {
            Bgr<byte>[,] image = ImageIO.LoadColor(fileName).Clone();
            Gray<byte>[,] grays = image.ToGray().CorrectContrast().Canny();
            grays.Save(@"D:/Data/test/outputs/canny_" + fileName.Split('/').Last());
        }
    }
}
