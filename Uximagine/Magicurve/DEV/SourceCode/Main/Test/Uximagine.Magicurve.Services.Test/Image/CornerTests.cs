using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using NUnit.Framework;
using Should;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing.Detectors;
using Uximagine.Magicurve.Image.Processing.Helpers;
using Uximagine.Magicurve.Image.Processing.ShapeCheckers;

namespace Uximagine.Magicurve.Services.Test.Image
{
    using System.Linq;

    using Accord.Imaging;
    using Accord.Imaging.Filters;

    using Uximagine.Magicurve.DataTransfer.Properties;

    /// <summary>
    ///     Test the corner detect algorithms
    /// </summary>
    [TestFixture]
    public class CornerTests
    {
        /// <summary>
        /// Tests the susan.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="cornerCount">The corner count.</param>
        [TestCase(@"D:/Data/test/inputs/combo_10.jpg", 6)]
        public void TestSusan(string fileName, int cornerCount)
        {
            Bitmap image = new Bitmap(fileName); // Lena's picture

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save(@"D:/Data/test/outputs/grayscale.jpg");

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/threshold.jpg");

            Median median = new Median();
            median.ApplyInPlace(image);

            //// var correctFormatImage = image.ConvertToFormat(PixelFormat.Format24bppRgb);

            Invert invert = new Invert();
            invert.ApplyInPlace(image);
            image.Save(@"D:/Data/test/outputs/invert.jpg");

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);
            List<Control> shapes = blobDetector.GetShapes();

            foreach (Control control in shapes)
            {
                Bitmap shape = control.EdgePoints.ConvertToBitmap();
                //// create corner detector's instance
                SusanCornersDetector scd = new SusanCornersDetector();
                //// create corner maker filter
                CornersMarker filter = new CornersMarker(scd, Color.Red);
                //// apply the filter
                filter.ApplyInPlace(shape);
                shape.Save(@"D:/Data/test/outputs/corners.jpg");

                List<IntPoint> corners = scd.ProcessImage(shape);
                IShapeChecker shapeChecker = new RuleBasedShapeChecker();
                ControlType type = shapeChecker.GetControlType(corners);
                Debug.WriteLine(type);
                type.ShouldEqual(ControlType.ComboBox);
            }
        }


        /// <summary>
        /// Tests the fast corners.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="cornerCount">The corner count.</param>
        [TestCase(@"D:\Data\test\inputs/button\test\test1.jpg", 4)]
        [TestCase(@"D:\Data\test\inputs/password\test\test1.jpg", 16)]
        [TestCase(@"D:\Data\test\inputs/combo\test\test1.jpg", 4)]
        [TestCase(@"D:\Data\test\inputs/checkbox\test\test1.jpg", 4)]
        [TestCase(@"D:\Data\test\inputs/checkbox\test\test2.jpg", 4)]
        [TestCase(@"D:\Data\test\inputs/date\test\test1.jpg", 4)]
        [TestCase(@"D:\Data\test\inputs/date\test\test2.jpg", 4)]
        [TestCase(@"D:\Data\test\inputs/hr\test\test2.jpg", 0)]
        [TestCase(@"D:\Data\test\inputs/iframe\test\test1.jpg", 4)]
        [TestCase(@"D:\Data\test\inputs/image\test\test2.jpg", 8)]
        [TestCase(@"D:\Data\test\inputs/label\test\test1.jpg", 8)]
        [TestCase(@"D:\Data\test\inputs/link\test\test1.jpg", 6)]
        [TestCase(@"D:\Data\test\inputs/paragraph\test\test1.jpg", 16)]
        [TestCase(@"D:\Data\test\inputs/range\test\test1.jpg", 0)]
        [TestCase(@"D:\Data\test\inputs/range\range2_03.jpg", 0)]
        [TestCase(@"D:\Data\test\inputs/radio\test\test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs/text\test\test1.jpg", 8)]
        public void TestFastCorners(string fileName, int cornerCount)
        {
            Bitmap image = new Bitmap(fileName); // Lena's picture

            image = image.GetBlobReady();

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);
            List<Control> shapes = blobDetector.GetShapes();
            shapes = shapes.Where(c => c.Width > 50 && c.Height > 50).ToList();

            foreach (Control control in shapes)
            {
                Bitmap shape = image.Crop(control.EdgePoints);

                //// create corner detector's instance
                FastCornersDetector cornersDetector = new FastCornersDetector(60);
                //// create corner maker filter
                CornersMarker filter = new CornersMarker(cornersDetector, Color.Red);
                //// apply the filter
                filter.ApplyInPlace(shape);
                shape.Save(@"D:/Data/test/outputs/corners_" + fileName.Split('/').Last().Split('\\').FirstOrDefault() + ".jpg");

                List<IntPoint> corners = cornersDetector.ProcessImage(shape);
                
                Debug.WriteLine($"corners found : {corners.Count}");
                corners.Count.ShouldEqual(cornerCount);
            }
        }

        /// <summary>
        /// Tests the fast retina key point.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="cornerCount">The corner count.</param>
        [TestCase(@"D:\Data\test\inputs/text\test\test1.jpg", 8)]
        public void TestFastRetinaKeypoint(string fileName, int cornerCount)
        {
            Bitmap image = new Bitmap(fileName); // Lena's picture

            image = image.GetBlobReady();

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);
            List<Control> shapes = blobDetector.GetShapes();
            shapes = shapes.Where(c => c.Width > 50 && c.Height > 50).ToList();
            Bitmap shape = image.Crop(shapes[0].EdgePoints);
            // The freak detector can be used with any other corners detection
            // algorithm. The default corners detection method used is the FAST
            // corners detection. So, let's start creating this detector first:
            // 
            var detector = new FastCornersDetector(20);

            // Now that we have a corners detector, we can pass it to the FREAK
            // feature extraction algorithm. Please note that if we leave this
            // parameter empty, FAST will be used by default.
            // 
            var freak = new FastRetinaKeypointDetector(detector);

            // Now, all we have to do is to process our image:
            List<FastRetinaKeypoint> points = freak.ProcessImage(shape);

            // Afterwards, we should obtain 83 feature points. We can inspect
            // the feature points visually using the FeaturesMarker class as
            // 
            FeaturesMarker marker = new FeaturesMarker(points, scale: 20);

            shape = marker.Apply(shape);

            shape.Save(@"D:/Data/test/outputs/corners_freak_" + fileName.Split('/').Last().Split('\\').FirstOrDefault() + ".jpg");

            // We can also inspect the feature vectors (descriptors) associated
            // with each feature point. In order to get a descriptor vector for
            // any given point, we can use
            // 
            byte[] feature = points[0].Descriptor;

            // By default, feature vectors will have 64 bytes in length. We can also
            // display those vectors in more readable formats such as HEX or base64
            // 
            string hex = points[0].ToHex();
            string b64 = points[0].ToBase64();

        }
    }
}