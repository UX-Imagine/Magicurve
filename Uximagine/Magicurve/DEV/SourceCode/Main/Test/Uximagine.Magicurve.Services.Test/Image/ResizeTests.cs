using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using AForge.Imaging.Filters;
using NUnit.Framework;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing.Detectors;
using Uximagine.Magicurve.Image.Processing.Helpers;

namespace Uximagine.Magicurve.Services.Test.Image
{
    /// <summary>
    /// Tests for resizing images.
    /// </summary>
    [TestFixture]
    public class ResizeTests
    {
        /// <summary>
        /// Tests the neighbor resize.
        /// </summary>
        [Test]
        [TestCase(@"D:/Data/test/inputs/combo_12.jpg", 100, 100)]
        [TestCase(@"D:/Data/test/inputs/template4.jpg", 100, 100)]
        public void TestNeighborResize(string fileName, int width, int height)
        {
            Bitmap image = new Bitmap(fileName);
            ResizeBicubic resize = new ResizeBicubic(width, height);
            Bitmap resized = resize.Apply(image);
            resized.Save(@"D:/Data/test/outputs/resized_" + fileName.Split('/').Last());
        }

        /// <summary>
        /// Tests the neighbor resize.
        /// </summary>
        [Test]
        [TestCase(@"D:/Data/test/inputs/combo_12.jpg", 100, 100)]
        [TestCase(@"D:/Data/test/inputs/template4.jpg", 100, 100)]
        public void TestBlobNeighborResize(string fileName, int width, int height)
        {
            Bitmap image = new Bitmap(fileName);

            IBlobDetector blobDetector = new HullBlobDetector();
            IEdgeDetector edgeDetector = new CannyDetector();
            
            Bitmap edges = edgeDetector.GetImage(image);
            Bitmap correctFormatImage = edges.ConvertToFormat(PixelFormat.Format24bppRgb);
            blobDetector.ProcessImage(correctFormatImage);
            List<Control> controls = blobDetector.GetShapes();
            Bitmap cropped = correctFormatImage.Crop(controls.Where(t => t.Height >= height && t.Width >= width).ToList()[1].EdgePoints);
            cropped.Save(@"D:/Data/test/outputs/resized_" + fileName.Split('/').Last());

            ResizeBicubic resize = new ResizeBicubic(width, height);
            Bitmap resized = resize.Apply(cropped.ConvertToFormat(PixelFormat.Format24bppRgb));
            resized.Save(@"D:/Data/test/outputs/resized_" + fileName.Split('/').Last());

        }

        /// <summary>
        /// Fits to size test.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        [TestCase(@"D:/Data/test/inputs/combo_12.jpg")]
        [TestCase(@"D:/Data/test/inputs/croped_0mix.jpg")]
        public void FitToSizeTest(string fileName)
        {
            Bitmap image = new Bitmap(fileName);
            System.Drawing.Image resizedImage = image.FitToSize(32, 32);
            Bitmap resized = new Bitmap(resizedImage, resizedImage.Width, resizedImage.Height);
            resized.Save(@"D:/Data/test/outputs/resized_" + fileName.Split('/').Last());
        }

        [TestCase(@"D:/Data/test/inputs/combo_12.jpg")]
        [TestCase(@"D:/Data/test/inputs/croped_0mix.jpg")]
        public void TestSimpleResize(string fileName)
        {
            Bitmap image = new Bitmap(fileName);
            Bitmap resized = new Bitmap(image, 32, 32);
            resized.Save(@"D:/Data/test/outputs/resized_simple_" + fileName.Split('/').Last());
        }

        [TestCase(@"D:/Data/test/inputs/combo_12.jpg")]
        [TestCase(@"D:/Data/test/inputs/croped_0mix.jpg")]
        public void TestQualityResize(string fileName)
        {
            Bitmap image = new Bitmap(fileName);
            Bitmap resized = image.Resize(32, 32);
            resized.Save(@"D:/Data/test/outputs/resized_quality_" + fileName.Split('/').Last());
        }
    }
}