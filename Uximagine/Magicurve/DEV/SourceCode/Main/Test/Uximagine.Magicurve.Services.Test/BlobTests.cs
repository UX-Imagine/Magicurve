// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BlobTests.cs" company="Uximagine">
//   ux-imagine 2015.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;
using AForge.Imaging.Filters;
using NUnit.Framework;
using Should;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing.Detectors;
using Uximagine.Magicurve.Image.Processing.Helpers;

namespace Uximagine.Magicurve.Services.Test
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
            Bitmap result = null;

            IBlobDetector blobDetector = new HullBlobDetector();

            //IEdgeDetector edgeDetector = new CannyDetector();

            var bitmap = new Bitmap("template.jpg");

            //var edgeResult = edgeDetector.GetImage(bitmap);

            //var correctFormatImage = edgeResult.ConvertToFormat(PixelFormat.Format24bppRgb);
            Bitmap image = Grayscale.CommonAlgorithms.BT709.Apply(bitmap);
            Threshold filterThreshold = new Threshold(79);
            filterThreshold.ApplyInPlace(image);
            Invert invert = new Invert();
            invert.ApplyInPlace(image);
            image.Save("thresh.jpg");
            blobDetector.ProcessImage(image);

            result = blobDetector.GetImage();

            result.Save("new.jpg");

            result.ShouldNotBeNull();

            result.Dispose();

            bitmap.Dispose();
        }

        /// <summary>
        /// Tests the BLOB detector.
        /// </summary>
        [TestCase]
        public void TestBlobDetector()
        {
            Bitmap result = null;

            IBlobDetector blobDetector = new BlobDetector();

            IEdgeDetector edgeDetector = new CannyDetector();

            var bitmap = new Bitmap("template.jpg");

            var edgeResult = edgeDetector.GetImage(bitmap);

            var correctFormatImage = edgeResult.ConvertToFormat(PixelFormat.Format24bppRgb);

            blobDetector.ProcessImage(correctFormatImage);

            result = blobDetector.GetImage();

            result.Save("new.jpg");

            result.ShouldNotBeNull();

            result.Dispose();

            bitmap.Dispose();
        }

        /// <summary>
        /// Detects the lines test.
        /// </summary>
        [Test]
        public void DetectLinesTest()
        {
            IBlobDetector blobDetector = new HullBlobDetector();

            IEdgeDetector edgeDetector = new CannyDetector();

            var bitmap = new Bitmap("template.jpg");

            var edgeResult = edgeDetector.GetImage(bitmap);

            //var correctFormatImage = edgeResult.ConvertToFormat(PixelFormat.Format24bppRgb);

            HoughLineTransformation lineTransform = new HoughLineTransformation();

            // apply Hough line transofrm
            lineTransform.ProcessImage(edgeResult);

            Bitmap houghLineImage = lineTransform.ToBitmap();

            houghLineImage.Save("hough.jpg");
        }

        /// <summary>
        ///     The should test blob count.
        /// </summary>
        [TestCase]
        public void DetectAndShowBlobTest()
        {
            List<Control> result = null;

            IBlobDetector blobDetector = new BlobDetector();

            IEdgeDetector edgeDetector = new CannyDetector();

            var bitmap = new Bitmap("template.jpg");

            var edgeResult = edgeDetector.GetImage(bitmap);

            var correctFormatImage = edgeResult.ConvertToFormat(PixelFormat.Format24bppRgb);
            blobDetector.ProcessImage(correctFormatImage);
            result = blobDetector.GetShapes();

            foreach (var item in result)
            {
                Debug.Write(item.Height);
            }

            result.Count.ShouldBeGreaterThanOrEqualTo(1);
            result.ShouldNotBeNull();
            
            bitmap.Dispose();
        }
    }
}