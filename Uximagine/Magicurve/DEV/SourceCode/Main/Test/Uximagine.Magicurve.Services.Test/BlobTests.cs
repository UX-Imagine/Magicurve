// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BlobTests.cs" company="Uximagine">
//   ux-imagine 2015.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Uximagine.Magicurve.Services.Test
{
    using System.Drawing;
    using Should;
    using Uximagine.Magicurve.Image.Processing;
    using Uximagine.Magicurve.Image.Processing.Detectors;
    using NUnit.Framework;
    using System.Drawing.Imaging;
    using Uximagine.Magicurve.Image.Processing.Helpers;
    using System.Collections.Generic;
    using Uximagine.Magicurve.Core.Shapes;
    using System;
    using System.Diagnostics;
    using Uximagine.Magicurve.Core.Diagnostics.Logging;

    /// <summary>
    /// The blob tests.
    /// </summary>
    [TestFixture]
    public class BlobTests
    {
        /// <summary>
        /// The should test blob count.
        /// </summary>
        [TestCase]
        public void DetectAndSaveTest()
        {
            Bitmap result = null;

            IBlobDetector blobDetector = new BlobDetector();

            IEdgeDetector edgeDetector = new CannyDetector();

            Bitmap bitmap = new Bitmap("capture.jpg");

            Bitmap edgeResult = edgeDetector.Detect(bitmap);

            Bitmap correctFormatImage = edgeResult.ConvertToFormat(PixelFormat.Format24bppRgb);

            result = blobDetector.Detect(correctFormatImage);

            result.Save("new.jpg");

            result.ShouldNotBeNull();

            result.Dispose();

            bitmap.Dispose();            
        }

        /// <summary>
        /// The should test blob count.
        /// </summary>
        [TestCase]
        public void DetectAndShowBlobTest()
        {
            List<Control> result = null;

            IBlobDetector blobDetector = new BlobDetector();

            IEdgeDetector edgeDetector = new CannyDetector();

            Bitmap bitmap = new Bitmap("template.jpg");

            Bitmap edgeResult = edgeDetector.Detect(bitmap);

            Bitmap correctFormatImage = edgeResult.ConvertToFormat(PixelFormat.Format24bppRgb);

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
