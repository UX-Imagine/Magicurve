using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uximagine.Magicurve.Image.Processing.Detectors;
using System.Drawing;
using Uximagine.Magicurve.Image.Processing.Matchers;

namespace Uximagine.Magicurve.Services.Test
{
    [TestClass]
    public class ImageProcessorTest
    {
        [TestMethod]
        public void TestTemplateMatcher()
        {
            Bitmap template =  new Bitmap("template.jpg");
            Bitmap source =  new Bitmap("capture.jpg");

            EdgeDetector edgeDetect = new EdgeDetector();

            Bitmap edgedTemplate = edgeDetect.Detect(template);
            Bitmap edgedSource = edgeDetect.Detect(source);

            TemplateMatcher detector = new TemplateMatcher();

            detector.Template = edgedTemplate;

            Bitmap image = detector.Match(edgedSource);

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

            BlockMatcher matcher = new BlockMatcher() { Template = template };
            Bitmap image = matcher.Match(source);

            Assert.IsNotNull(image);
        }
    }
}
