using System.Drawing;
using System.Linq;
using NUnit.Framework;
using Should;
using Uximagine.Magicurve.Image.Processing.Common;
using Uximagine.Magicurve.Image.Processing.Helpers;

namespace Uximagine.Magicurve.Services.Test.Image
{
    /// <summary>
    /// Basic Image Processing Tests
    /// </summary>
    [TestFixture]
    public class BasicImageProcessingTests
    {
        /// <summary>
        /// Tests the color filter.
        /// </summary>
        [TestCase(@"D:/Data/test/inputs/template.jpg", ColorFilterType.Red)]
        public void TestColorFilter(string fileName, ColorFilterType filterType)
        {
            Bitmap bmap = new Bitmap(fileName);
            Bitmap filtered = bmap.SetColorFilter(filterType);
            filtered.Save(@"D:/Data/test/outputs/filtered.jpg");
        }

        /// <summary>
        /// Tests the color filter.
        /// </summary>
        [TestCase(@"D:/Data/test/inputs/template.jpg", ColorFilterType.Red)]
        [TestCase(@"D:/Data/test/inputs/template.jpg", ColorFilterType.Green)]
        [TestCase(@"D:/Data/test/inputs/template.jpg", ColorFilterType.Blue)]
        public void TestColorFilterV2(string fileName, ColorFilterType filterType)
        {
            Bitmap bmap = new Bitmap(fileName);
            Bitmap filtered = bmap.SetColorFilterV2(filterType);
            filtered.Save(@"D:/Data/test/outputs/filtered" + filterType + ".jpg");
        }

        /// <summary>
        /// Tests the invert filter.
        /// </summary>
        [TestCase(@"D:/Data/test/inputs/template.jpg")]
        public void TestInvertFilter(string fileName)
        {
            Bitmap bmap = new Bitmap(fileName);
            Bitmap filtered = bmap.Invert();
            filtered.Save(@"D:/Data/test/outputs/inverted.jpg");
        }

        /// <summary>
        /// Tests the invert filter.
        /// </summary>
        [TestCase(@"D:/Data/test/inputs/template.jpg")]
        public void TestGrayScaleFilter(string fileName)
        {
            Bitmap bmap = new Bitmap(fileName);
            Bitmap filtered = bmap.Grayscale();
            filtered.Save(@"D:/Data/test/outputs/gray.jpg");
        }

        /// <summary>
        /// Tests the invert filter.
        /// </summary>
        [TestCase(@"D:/Data/test/inputs/template.jpg", 50)]
        [TestCase(@"D:/Data/test/inputs/template.jpg", -50)]
        public void TestBrightenessFilter(string fileName, int brightness)
        {
            Bitmap bmap = new Bitmap(fileName);
            Bitmap filtered = bmap.Brighten(brightness);
            filtered.Save(@"D:/Data/test/outputs/brighten" + brightness + ".jpg");
        }

        /// <summary>
        /// Tests the invert filter.
        /// </summary>
        [TestCase(@"D:/Data/test/inputs/template.jpg", 30)]
        [TestCase(@"D:/Data/test/inputs/template.jpg", -30)]
        public void TestContrastFilter(string fileName, int contrast)
        {
            Bitmap bmap = new Bitmap(fileName);
            Bitmap filtered = bmap.Contrast(contrast);
            filtered.Save(@"D:/Data/test/outputs/contrast" + contrast + ".jpg");
        }

        /// <summary>
        /// Tests the Gamma filter.
        /// </summary>
        [TestCase(@"D:/Data/test/inputs/template.jpg")]
        [TestCase(@"D:/Data/test/inputs/template.jpg")]
        public void TestGammaFilter(string fileName)
        {
            Bitmap bmap = new Bitmap(fileName);
            Bitmap filtered = bmap.Gamma(2.5, 2.5, 2.5);
            filtered.Save(@"D:/Data/test/outputs/gamma.jpg");
        }

        /// <summary>
        /// Tests the smooth filter.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="amount">The amount.</param>
        [TestCase(@"D:/Data/test/inputs/Hannah.jpg", 8)]
        [TestCase(@"D:/Data/test/inputs/template.jpg", 4)]
        public void TestSmoothFilter(string fileName, int amount)
        {
            Bitmap bmap = new Bitmap(fileName);
            var result = bmap.Smooth(amount);
            result.Save(@"D:/Data/test/outputs/smoothed" + fileName.Split('/').Last());
        }

        /// <summary>
        /// Tests the gaussian filter.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        [TestCase(@"D:/Data/test/inputs/template.jpg")]
        [TestCase(@"D:/Data/test/inputs/Hannah.jpg")]
        public void TestGaussianFilter(string fileName)
        {
            Bitmap bmap = new Bitmap(fileName);
            var result = bmap.GaussianBlur();
            result.Save(@"D:/Data/test/outputs/gaussian" + fileName.Split('/').Last());
        }

        /// <summary>
        /// Tests the gaussian filter.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        [TestCase(@"D:/Data/test/inputs/template.jpg")]
        [TestCase(@"D:/Data/test/inputs/HannahGaussian.jpg")]
        public void TestSharpenFilter(string fileName)
        {
            Bitmap bmap = new Bitmap(fileName);
            var result = bmap.GaussianBlur();
            result.Save(@"D:/Data/test/outputs/sharpen" + fileName.Split('/').Last());
        }

        /// <summary>
        /// Tests the eges filter.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        [TestCase(@"D:/Data/test/inputs/HannahGaussian.jpg")]
        public void TestEgesFilter(string fileName)
        {
            Bitmap bmap = new Bitmap(fileName);
            var result = bmap.EdgeDetect();
            result.Save(@"D:/Data/test/outputs/edges" + fileName.Split('/').Last());

        }

        /// <summary>
        /// Tests the eges v2 filter.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        [TestCase(@"D:/Data/test/inputs/HannahGaussian.jpg")]
        public void TestEgesV2Filter(string fileName)
        {
            Bitmap bmap = new Bitmap(fileName);
            ConvolutionFilterHelper.EdgeDetectV2(bmap);
            bmap.Save(@"D:/Data/test/outputs/edges" + fileName.Split('/').Last());
        }

        /// <summary>
        /// Tests the embose filter.
        /// </summary>
        /// <param name="fileName">
        /// Name of the file.
        /// </param>
        [TestCase(@"D:/Data/test/inputs/HannahGaussian.jpg")]
        public void TestEmboseFilter(string fileName)
        {
            Bitmap bmap = new Bitmap(fileName);
            var result = bmap.Embose();
            result.Save(@"D:/Data/test/outputs/embose" + fileName.Split('/').Last());
        }

        /// <summary>
        /// Tests the embose filter.
        /// </summary>
        /// <param name="fileName">
        /// Name of the file.
        /// </param>
        [TestCase(@"D:/Data/test/inputs/HannahGaussian.jpg")]
        [TestCase(@"D:/Data/test/inputs/template.jpg")]
        [TestCase(@"D:/Data/test/inputs/template4.jpg")]
        public void TestSobelFilter(string fileName)
        {
            Bitmap bmap = new Bitmap(fileName);
            var gray = bmap.Grayscale();
            var result = gray.ApplySobel();
            result.Save(@"D:/Data/test/outputs/sobel" + fileName.Split('/').Last());
        }

        [TestCase(@"D:/Data/test/inputs/template4.jpg", (byte)127)]
        [TestCase(@"D:/Data/test/inputs/template.jpg", (byte)0)]
        public void TestHomogenityFilter(string fileName, byte threshold)
        {
            Bitmap bmap = new Bitmap(fileName);
            var gray = bmap.Grayscale();
            var result = gray.EdgeDetectHomogenity(threshold);
            result.Save(@"D:/Data/test/outputs/homo" + fileName.Split('/').Last());
        }

        /// <summary>
        /// Tests the horizontal edges.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        [TestCase(@"D:/Data/test/inputs/template.jpg")]
        [TestCase(@"D:/Data/test/inputs/template4.jpg")]
        public void TestHorizontalEdges(string fileName)
        {
            Bitmap bmap = new Bitmap(fileName);
            var gray = bmap.Grayscale();
            var result = gray.HorizontalEdges();
            result.Save(@"D:/Data/test/outputs/hEdges" + fileName.Split('/').Last());
        }

        /// <summary>
        /// Tests the vertical edges.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        [TestCase(@"D:/Data/test/inputs/template.jpg")]
        [TestCase(@"D:/Data/test/inputs/template4.jpg")]
        public void TestVerticalEdges(string fileName)
        {
            Bitmap bmap = new Bitmap(fileName);
            var gray = bmap.Grayscale();
            var result = gray.VerticalEdges();
            result.Save(@"D:/Data/test/outputs/vEdges" + fileName.Split('/').Last());
        }

        /// <summary>
        /// Tests the difference egde detect.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="threshold">The threshold.</param>
        [TestCase(@"D:/Data/test/inputs/template4.jpg", (byte)127)]
        [TestCase(@"D:/Data/test/inputs/template.jpg", (byte)0)]
        public void TestDifferenceEgdeDetect(string fileName, byte threshold)
        {
            Bitmap bmap = new Bitmap(fileName);
            var gray = bmap.Grayscale();
            var result = gray.EdgeDetectDifference(threshold);
            result.ShouldBeTrue();
            gray.Save(@"D:/Data/test/outputs/difference_" + fileName.Split('/').Last());
        }

        /// <summary>
        /// Tests the enhance edge.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="threshold">The threshold.</param>
        [TestCase(@"D:/Data/test/inputs/template4.jpg", (byte)127)]
        [TestCase(@"D:/Data/test/inputs/template.jpg", (byte)0)]
        public void TestEnhanceEdge(string fileName, byte threshold)
        {
            Bitmap bmap = new Bitmap(fileName);
            var gray = bmap.Grayscale();
            var result = gray.EdgeEnhancement(threshold);
            result.ShouldBeTrue();
            gray.Save(@"D:/Data/test/outputs/enhanced_" + fileName.Split('/').Last());
        }
    }
}
