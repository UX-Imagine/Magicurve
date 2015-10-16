// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BlobTests.cs" company="Uximagine">
//   ux-imagine 2015.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
        public void DetectAndSaveTest()
        {
            Bitmap result = null;

            IBlobDetector blobDetector = new BlobDetector();

            IEdgeDetector edgeDetector = new CannyDetector();

            var bitmap = new Bitmap("capture.jpg");

            var edgeResult = edgeDetector.GetImage(bitmap);

            var correctFormatImage = edgeResult.ConvertToFormat(PixelFormat.Format24bppRgb);

            result = blobDetector.GetImage(correctFormatImage);

            result.Save("new.jpg");

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
            List<Control> result = null;

            IBlobDetector blobDetector = new BlobDetector();

            IEdgeDetector edgeDetector = new CannyDetector();

            var bitmap = new Bitmap("template.jpg");

            var edgeResult = edgeDetector.GetImage(bitmap);

            var correctFormatImage = edgeResult.ConvertToFormat(PixelFormat.Format24bppRgb);

            result = blobDetector.GetShapes(correctFormatImage);

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