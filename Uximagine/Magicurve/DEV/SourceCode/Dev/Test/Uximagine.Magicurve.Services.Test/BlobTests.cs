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

    /// <summary>
    /// The blob tests.
    /// </summary>
    public class BlobTests
    {
        /// <summary>
        /// The should test blob count.
        /// </summary>
        public void ShouldTestBlobCount()
        {
            IBlobDetector blobDetector = ProcessingFactory.GetBlobDetector();

            Bitmap source = new Bitmap("capture.jpg");

            Bitmap result = blobDetector.Detect(source);

            result.ShouldNotBeNull();
        }
}
}
