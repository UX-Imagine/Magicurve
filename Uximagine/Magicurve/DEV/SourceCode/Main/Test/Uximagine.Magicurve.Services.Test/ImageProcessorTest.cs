using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uximagine.Magicurve.Image.Processing.Detectors;
using System.Drawing;

namespace Uximagine.Magicurve.Services.Test
{
    [TestClass]
    public class ImageProcessorTest
    {
        [TestMethod]
        public void TestTemplateDetector()
        {
            Bitmap template =  new Bitmap("template.jpg");
            Bitmap source =  new Bitmap("capture.jpg");

            EdgeDetector edgeDetect = new EdgeDetector();

            Bitmap edgedTemplate = edgeDetect.Detect(template);
            Bitmap edgedSource = edgeDetect.Detect(source);

            TemplateDetector detector = new TemplateDetector();

            detector.TemplateImage = edgedTemplate;

            Bitmap image = detector.Detect(edgedSource);

            Assert.IsNotNull(image);
        }
    }
}
