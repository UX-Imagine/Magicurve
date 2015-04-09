namespace Uximagine.Magicurve.Services.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Drawing;
    using Uximagine.Magicurve.Image.Processing;
    using Uximagine.Magicurve.Image.Processing.Detectors;
    using Uximagine.Magicurve.Image.Processing.Matchers;

    /// <summary>
    /// Image processor test.
    /// </summary>
    [TestClass]
    public class ImageProcessorTest
    {
        /// <summary>
        /// Tests the template matcher.
        /// </summary>
        [TestMethod]
        public void TestTemplateMatcher()
        {
            Bitmap template = new Bitmap("template.jpg");
            Bitmap source = new Bitmap("capture.jpg");

            IDetector edgeDetect = ProcessingFactory.GetEdgeDetector();

            Bitmap edgedTemplate = edgeDetect.Detect(template);
            Bitmap edgedSource = edgeDetect.Detect(source);

            IMatcher matcher = ProcessingFactory.GetMatcher();

            matcher.Template = edgedTemplate;

            Bitmap image = matcher.Match(edgedSource);

            Assert.IsNotNull(image);
        }

        /// <summary>
        /// Tests the block matcher.
        /// </summary>
        [TestMethod]
        public void TestBlockMatcher()
        {
            Bitmap template = new Bitmap("template.jpg");
            Bitmap source = new Bitmap("capture.jpg");

            IMatcher matcher = ProcessingFactory.GetMatcher();
            matcher.Template = template ;
            Bitmap image = matcher.Match(source);

            Assert.IsNotNull(image);
        }
    }
}
