using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Accord.Imaging.Filters;
using AForge.Imaging;
using AForge.Imaging.Filters;
using NUnit.Framework;
using Should;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Image.Processing.Detectors;
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
            var image = new Bitmap(@"D:/Data/test/inputs/template.jpg"); // Lena's picture

            // Create a new Kirsch's edge detector:
            var kirsch = new KirschEdgeDetector();
            
            // Compute the image edges
            var edges = kirsch.Apply(image);

            // Save File
            edges.Save(@"D:/Data/test/outputs/kirsch_edges.jpg");
        }

        /// <summary>
        /// Cannies the test.
        /// </summary>
        [Test]
        public void CannyTest()
        {
            var image = new Bitmap(@"D:/Data/test/inputs/template.jpg"); // Lena's picture

            image.SetResolution(60f,60f);

            image = new Bitmap(image, 1024, 768);

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save(@"D:/Data/test/outputs/grayscale.jpg");

            var threshold = new Threshold(79);
            threshold.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/threshold.jpg");

            //Dilatation dilatation = new Dilatation();
            // apply the filter
            //dilatation.ApplyInPlace(image);
            //image.Save("dilate.jpg");

            var closing = new Closing();
            closing.ApplyInPlace(image);
            image.Save(@"D:/Data/test/outputs/closing.jpg");

            Median median = new Median(25);
            median.ApplyInPlace(image);
            image.Save(@"D:/Data/test/outputs/median.jpg");

            var overlay = AForge.Imaging.Image.Clone(image).ConvertToFormat(PixelFormat.Format24bppRgb);

            var euclidean = new EuclideanColorFiltering
            {
                CenterColor = new AForge.Imaging.RGB(Color.White),  //Pure White
                Radius = 0,                                         //Increase this to allow off-whites
                FillColor = new AForge.Imaging.RGB(Color.White)       //Replacement Colour
            };
            
            euclidean.ApplyInPlace(overlay);

            overlay.Save(@"D:/Data/test/outputs/overlay.jpg");

            Merge merge = new Merge(overlay);
            var mergeImage = merge.Apply(image.ConvertToFormat(PixelFormat.Format24bppRgb));
            mergeImage.Save(@"D:/Data/test/outputs/merged.jpg");

            // Skeletonization.
            var filter = new SimpleSkeletonization();
            var skeliton = filter.Apply(image);
            skeliton.Save(@"D:/Data/test/outputs/skeliton.jpg");

            // Create a new Canny's edge detector:
            var edgeDetector = new CannyEdgeDetector(20, 100, 1.4);
            // Compute the image edges
            var edges = edgeDetector.Apply(image);
            edges.Save(@"D:/Data/test/outputs/canny.jpg");

            var sobel = new SobelEdgeDetector();
            var sobelImage = sobel.Apply(image);
            sobelImage.Save(@"D:/Data/test/outputs/sobel.jpg");

        }

        /// <summary>
        /// Tests the fill holes.
        /// </summary>
        [Test]
        public void TestFillHoles()
        {
            var image = new Bitmap(@"D:/Data/test/inputs/image_01.jpg"); // Lena's picture

            //image.SetResolution(60f, 60f);

            //image = new Bitmap(image, 1024, 768);

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save(@"D:/Data/test/outputs/grayscale.jpg");

            var threshold = new Threshold();
            threshold.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/threshold.jpg");

            var median = new Median();
            median.ApplyInPlace(image);
            image.Save(@"D:/Data/test/outputs/median.jpg");

            var filholes = new FillHoles
            {
                MaxHoleHeight = 20,
                MaxHoleWidth = 20,
                CoupledSizeFiltering = false
            };

            // apply the filter
            var filledImage = filholes.Apply(image);
            filledImage.Save(@"D:/Data/test/outputs/filled.jpg");
        }

        /// <summary>
        /// Tests the blobs.
        /// </summary>
        [TestCase(@"D:/Data/test/inputs/template4.jpg", 2, 1, 11, 0)]
        [TestCase(@"D:/Data/test/inputs/image_03.jpg", 0, 0, 1, 0)]
        [TestCase(@"D:/Data/test/inputs/template3.jpg", 0, 0, 0, 1)]
        [TestCase(@"D:/Data/test/inputs/combo_10.jpg", 0, 0, 0, 1)]
        public void TestBLobs(string fileName, int radioCount, int iFrameCount, int buttonCount, int comboCount = 0)
        {
            var image = new Bitmap(fileName); // Lena's picture

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save(@"D:/Data/test/outputs/grayscale.jpg");

            var threshold = new Threshold();
            threshold.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/threshold.jpg");

            var median = new Median();
            median.ApplyInPlace(image);

            //var correctFormatImage = image.ConvertToFormat(PixelFormat.Format24bppRgb);

            Invert invert = new Invert();
            invert.ApplyInPlace(image);
            image.Save(@"D:/Data/test/outputs/invert.jpg");

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            var result = blobDetector.GetImage();
            result.Save(@"D:/Data/test/outputs/blobs.jpg");

            var controls = blobDetector.GetShapes();
            controls.Where(t=> t.Type != ControlType.None).ToList().ForEach(
                (t) => Debug.WriteLine("{0} {1} {2} {3} {4}",t.Type, t.Width, t.Height, t.X, t.Y));

            controls.Count(t => t.Type == ControlType.RadioButton).ShouldEqual(radioCount);
            controls.Count(t => t.Type == ControlType.Iframe).ShouldEqual(iFrameCount);
            controls.Count(t => t.Type == ControlType.Button).ShouldEqual(buttonCount);
        }

        /// <summary>
        /// Tests the erroson.
        /// </summary>
        [Test]
        public void TestErroson()
        {
            var image = new Bitmap(@"D:/Data/test/inputs/template3.jpg"); // Lena's picture

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save(@"D:/Data/test/outputs/grayscale.jpg");

            var threshold = new Threshold();
            threshold.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/threshold.jpg");

            var median = new Median();
            median.ApplyInPlace(image);

            var erosion = new Erosion();
            erosion.ApplyInPlace(image);
            image.Save(@"D:/Data/test/outputs/erosion.jpg");

            var correctFormatImage = image.ConvertToFormat(PixelFormat.Format24bppRgb);

            Invert invert = new Invert();
            invert.ApplyInPlace(correctFormatImage);

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(correctFormatImage);

            var result = blobDetector.GetImage();
            var controls = blobDetector.GetShapes();
            Debug.WriteLine(controls);

            result.Save(@"D:/Data/test/outputs/blobs.jpg");
        }

        [Test]
        public void TestBLobsFiles()
        {
            var files = Directory.GetFiles(@"D:/Data/test/inputs");
            int index = 0;

            foreach (var file in files)
            {
                var image = new Bitmap(file); // Lena's picture

                IBlobDetector blobDetector = new BlobDetector();

                image = Grayscale.CommonAlgorithms.BT709.Apply(image);

                image.Save(@"D:/Data/test/outputs/grayscale_" + index + ".jpg");

                var threshold = new Threshold();
                threshold.ApplyInPlace(image);

                image.Save(@"D:/Data/test/outputs/threshold_" + index + ".jpg");

                var median = new Median();
                median.ApplyInPlace(image);

                var correctFormatImage = image.ConvertToFormat(PixelFormat.Format24bppRgb);

                Invert invert = new Invert();
                invert.ApplyInPlace(correctFormatImage);

                blobDetector.ProcessImage(correctFormatImage);

                var result = blobDetector.GetImage();
                var controls = blobDetector.GetShapes();
                Debug.WriteLine(controls);

                result.Save(@"D:/Data/test/outputs/blobs_" + index + ".jpg");
                index++;
            }
            
        }

        /// <summary>
        /// Tests the hough transform.
        /// </summary>
        [Test]
        public void TestHoughTransform()
        {
            var image = new Bitmap(@"D:/Data/test/inputs/image_02.jpg"); // Lena's picture

            IBlobDetector blobDetector = new BlobDetector();

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save(@"D:/Data/test/outputs/grayscale.jpg");

            var threshold = new Threshold();
            threshold.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/threshold.jpg");

            var median = new Median();
            median.ApplyInPlace(image);

            var correctFormatImage = image.ConvertToFormat(PixelFormat.Format24bppRgb);

            Invert invert = new Invert();
            invert.ApplyInPlace(correctFormatImage);

            HoughLineTransformation lineTransform = new HoughLineTransformation();
            
            // apply Hough line transofrm
            lineTransform.ProcessImage(image);
            Bitmap houghLineImage = lineTransform.ToBitmap();
            houghLineImage.Save(@"D:/Data/test/outputs/hough.jpg");

            foreach (var line in lineTransform.GetLinesByRelativeIntensity(0.75))
            {
                Debug.WriteLine(line.Theta);
            }
        }
    }
}