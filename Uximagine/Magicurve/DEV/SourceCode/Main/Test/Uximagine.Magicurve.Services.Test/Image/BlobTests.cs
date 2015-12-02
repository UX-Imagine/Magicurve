// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BlobTests.cs" company="Uximagine">
//   ux-imagine 2015.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Accord.Imaging;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using NUnit.Framework;
using Should;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing.Detectors;
using Uximagine.Magicurve.Image.Processing.Helpers;

namespace Uximagine.Magicurve.Services.Test.Image
{
    /// <summary>
    ///     The blob tests.
    /// </summary>
    [TestFixture]
    public class BlobTests
    {
        /// <summary>
        ///     The should test blob count.
        /// </summary>
        [TestCase]
        public void TestHullBlobDetector()
        {
            IBlobDetector blobDetector = new HullBlobDetector();

            IEdgeDetector edgeDetector = new CannyDetector();

            Bitmap bitmap = new Bitmap("inputs/template.jpg");

            Bitmap edgeResult = edgeDetector.GetImage(bitmap);

            edgeResult.ConvertToFormat(PixelFormat.Format24bppRgb);
            Bitmap image = Grayscale.CommonAlgorithms.BT709.Apply(bitmap);

            Threshold filterThreshold = new Threshold(79);
            filterThreshold.ApplyInPlace(image);

            Invert invert = new Invert();
            invert.ApplyInPlace(image);
            image.Save("outputs/thresh.jpg");

            blobDetector.ProcessImage(image);

            Bitmap result = blobDetector.GetImage();

            result.Save("outputs/blobs.jpg");

            result.ShouldNotBeNull();

            result.Dispose();

            bitmap.Dispose();
        }

        /// <summary>
        ///     Tests the BLOB detector.
        /// </summary>
        [TestCase]
        public void TestBlobDetector()
        {
            IBlobDetector blobDetector = new BlobDetector();

            IEdgeDetector edgeDetector = new CannyDetector();

            Bitmap bitmap = new Bitmap("inputs/template.jpg");

            Bitmap edgeResult = edgeDetector.GetImage(bitmap);

            Bitmap correctFormatImage = edgeResult.ConvertToFormat(PixelFormat.Format24bppRgb);

            blobDetector.ProcessImage(correctFormatImage);

            Bitmap result = blobDetector.GetImage();

            result.Save("outputs/blobs.jpg");

            result.ShouldNotBeNull();

            result.Dispose();

            bitmap.Dispose();
        }

        /// <summary>
        ///     The should test blob count.
        /// </summary>
        [TestCase]
        public void DetectAndShowBlobTest()
        {
            IBlobDetector blobDetector = new BlobDetector();

            IEdgeDetector edgeDetector = new CannyDetector();

            Bitmap bitmap = new Bitmap("template.jpg");

            Bitmap edgeResult = edgeDetector.GetImage(bitmap);

            Bitmap correctFormatImage = edgeResult.ConvertToFormat(PixelFormat.Format24bppRgb);
            blobDetector.ProcessImage(correctFormatImage);
            List<Control> result = blobDetector.GetShapes();

            foreach (Control item in result)
            {
                Debug.Write(item.Height);
            }

            result.Count.ShouldBeGreaterThanOrEqualTo(1);
            result.ShouldNotBeNull();

            bitmap.Dispose();
        }

        /// <summary>
        ///     Tests the border following.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        [TestCase(@"D:/Data/test/inputs/combo/test/combo2_01.jpg")]
        public void TestBorderFollowing(string fileName)
        {
            BorderFollowing detector = new BorderFollowing();
            Bitmap gray = new Bitmap(fileName).Grayscale().Invert();
            List<IntPoint> points = detector.FindContour(gray);
            Bitmap cropped = gray.Crop(points);
            cropped.Save(@"D:/Data/test/outputs/blobfollow_" + fileName.Split('/').Last());

            GaussianSharpen gaussian = new GaussianSharpen(4, 11);
            Bitmap gaussianed = gaussian.Apply(cropped);
            gaussianed.Save(@"D:/Data/test/outputs/gau_" + fileName.Split('/').Last());

            GaussianBlur gaussianBlur = new GaussianBlur(4, 11);
            Bitmap blured = gaussianBlur.Apply(cropped);
            blured.Save(@"D:/Data/test/outputs/blur_" + fileName.Split('/').Last());

            blured.HorizontalEdges().Save(@"D:/Data/test/outputs/horizontal_" + fileName.Split('/').Last());
            blured.VerticalEdges().Save(@"D:/Data/test/outputs/ver_" + fileName.Split('/').Last());
        }

        /// <summary>
        ///     Detects the lines test.
        /// </summary>
        [TestCase(@"D:/Data/test/inputs/combo/test/combo2_01.jpg")]
        public void DetectLinesTest(string fileName)
        {
            IEdgeDetector edgeDetector = new CannyDetector();

            Bitmap bitmap = new Bitmap(fileName);

            Bitmap edgeResult = edgeDetector.GetImage(bitmap);

            //var correctFormatImage = edgeResult.ConvertToFormat(PixelFormat.Format24bppRgb);

            HoughLineTransformation lineTransform = new HoughLineTransformation();

            // apply Hough line transofrm
            lineTransform.ProcessImage(edgeResult);

            Bitmap houghLineImage = lineTransform.ToBitmap();

            houghLineImage.Save(@"D:/Data/test/outputs/hough_" + fileName.Split('/').Last());
        }

        /// <summary>
        /// Tests the canny and BLOB.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        [TestCase(@"D:/Data/test/inputs/combo/test/combo2_01.jpg")]
        public void TestCannyAndBlob(string fileName)
        {
            IBlobDetector blobDetector = new HullBlobDetector();
            IEdgeDetector edgeDetector = new SobelDetector();

            Bitmap image = new Bitmap(fileName);

            Bitmap gray = image.Grayscale();

            Bitmap edges = edgeDetector.GetImage(gray);

            blobDetector.ProcessImage(edges);
            Bitmap blobs = blobDetector.GetImage();

            SimpleSkeletonization skeletonization = new SimpleSkeletonization();
            Bitmap sekliton = skeletonization.Apply(edges.ConvertToFormat(PixelFormat.Format8bppIndexed));

            edges.Save(@"D:/Data/test/outputs/edges_" + fileName.Split('/').Last());
            blobs.Save(@"D:/Data/test/outputs/blobs_" + fileName.Split('/').Last());
            sekliton.Save(@"D:/Data/test/outputs/skel_" + fileName.Split('/').Last());
        }

        [TestCase(@"D:/Data/test/inputs/combo/test/combo2_01.jpg")]
        public void TestBlobReady(string fileName)
        {
            Bitmap image = new Bitmap(fileName);
            image = image.GetBlobReady();
            image.Save(@"D:/Data/test/outputs/blobsReady_" + fileName.Split('/').Last());
        }
    }
}