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

            IDetector edgeDetect = DetectorFactory.GetEdgeDetector();

            Bitmap edgedTemplate = edgeDetect.Detect(template);
            Bitmap edgedSource = edgeDetect.Detect(source);

            IMatcher matcher = MatcherFactory.GetTemplateMatcher();

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

            IMatcher matcher = MatcherFactory.GetBlockMatcher();
            matcher.Template = template ;
            Bitmap image = matcher.Match(source);

            Assert.IsNotNull(image);
        }
    }
}
