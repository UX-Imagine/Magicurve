using System.Drawing;
using System.Drawing.Imaging;
using Accord.Imaging.Filters;
using AForge.Imaging.Filters;
using NUnit.Framework;
using Uximagine.Magicurve.Image.Processing.Helpers;

namespace Uximagine.Magicurve.Services.Test.Image
{
    /// <summary>
    /// The edge detection algorithm tests.
    /// </summary>
    [TestFixture]
    public class EdgeTests
    {
        /// <summary>
        /// Kirsches the test.
        /// </summary>
        [Test]
        public void KirschTest()
        {
            var image = new Bitmap("template.jpg"); // Lena's picture

            // Create a new Kirsch's edge detector:
            var kirsch = new KirschEdgeDetector();
            
            // Compute the image edges
            var edges = kirsch.Apply(image);

            // Save File
            edges.Save("kirsch_edges.jpg");
        }

        /// <summary>
        /// Cannies the test.
        /// </summary>
        [Test]
        public void CannyTest()
        {
            var image = new Bitmap("template.jpg"); // Lena's picture

            image.SetResolution(60f,60f);

            image = new Bitmap(image, 1024, 768);

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save("grayscale.jpg");

            var threshold = new Threshold(79);
            threshold.ApplyInPlace(image);

            image.Save("threshold.jpg");

            //Dilatation dilatation = new Dilatation();
            // apply the filter
            //dilatation.ApplyInPlace(image);
            //image.Save("dilate.jpg");

            var closing = new Closing();
            closing.ApplyInPlace(image);
            image.Save("closing.jpg");

            Median median = new Median(25);
            median.ApplyInPlace(image);
            image.Save("median.jpg");

            var overlay = AForge.Imaging.Image.Clone(image).ConvertToFormat(PixelFormat.Format24bppRgb);

            var euclidean = new EuclideanColorFiltering
            {
                CenterColor = new AForge.Imaging.RGB(Color.White),  //Pure White
                Radius = 0,                                         //Increase this to allow off-whites
                FillColor = new AForge.Imaging.RGB(Color.White)       //Replacement Colour
            };
            
            euclidean.ApplyInPlace(overlay);

            overlay.Save("overlay.jpg");

            Merge merge = new Merge(overlay);
            var mergeImage = merge.Apply(image.ConvertToFormat(PixelFormat.Format24bppRgb));
            mergeImage.Save("merged.jpg");

            // Skeletonization.
            var filter = new SimpleSkeletonization();
            var skeliton = filter.Apply(image);
            skeliton.Save("skeliton.jpg");

            // Create a new Canny's edge detector:
            var edgeDetector = new CannyEdgeDetector(20, 100, 1.4);
            // Compute the image edges
            var edges = edgeDetector.Apply(image);
            edges.Save("canny.jpg");

            var sobel = new SobelEdgeDetector();
            var sobelImage = sobel.Apply(image);
            sobelImage.Save("sobel.jpg");

        }

        /// <summary>
        /// Tests the fill holes.
        /// </summary>
        [Test]
        public void TestFillHoles()
        {
            var image = new Bitmap("template.jpg"); // Lena's picture

            image.SetResolution(60f, 60f);

            image = new Bitmap(image, 1024, 768);

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save("grayscale.jpg");

            var threshold = new Threshold(79);
            threshold.ApplyInPlace(image);

            image.Save("threshold.jpg");

            var median = new Median(25);
            median.ApplyInPlace(image);
            image.Save("median.jpg");

            var filter = new FillHoles
            {
                MaxHoleHeight = 20,
                MaxHoleWidth = 20,
                CoupledSizeFiltering = false
            };

            // apply the filter
            var result = filter.Apply(image);
            result.Save("filled.jpg");
        }
    }
}