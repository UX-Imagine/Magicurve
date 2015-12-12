namespace Uximagine.Magicurve.Services.Test.Image
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

    using Should;

    using Uximagine.Magicurve.Core.Shapes;
    using Uximagine.Magicurve.Image.Processing.Common;
    using Uximagine.Magicurve.Image.Processing.Detectors;
    using Uximagine.Magicurve.Image.Processing.Helpers;

    /// <summary>
    /// The line counter tests.
    /// </summary>
    public class LineCounterTests
    {
        /// <summary>
        /// Gets the horizontal line count.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// The horizontal lines count.
        /// </returns>
        public int GetHorizontalLineCount(string fileName)
        {
            int count;
            Bitmap image = new Bitmap(fileName);

            image = image.GetBlobReady();

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            List<Control> controls = blobDetector.GetShapes();
            image = image.Crop(controls.Where(c => c.Width > 50 && c.Height > 50).ToList()[0].EdgePoints);

            image.ConvolutionFilter(FilterMatrix.Prewitt3x3Horizontal);

            image.ToBinary();

            image.GetHorizontalLinesCount(out count);

            return count;
        }

        /// <summary>
        /// Gets the vertical line count.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// The vertical lines count.
        /// </returns>
        public int GetVerticalLineCount(string fileName)
        {
            int count;
            Bitmap image = new Bitmap(fileName);

            image = image.GetBlobReady();

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            List<Control> controls = blobDetector.GetShapes();
            image = image.Crop(controls.Where(c => c.Width > 50 && c.Height > 50).ToList()[0].EdgePoints);

            image.ConvolutionFilter(FilterMatrix.Prewitt3x3Vertical);

            image.ToBinary();

            image.GetVerticalLinesCount(out count);

            return count;
        }

        [TestCase(@"D:/Data/test/inputs/button/test/test1.jpg", 2, 2)]
        [TestCase(@"D:\Data\test\inputs\password\test/test1.jpg", 2, 4)]
        [TestCase(@"D:\Data\test\inputs\combo\test/test1.jpg", 2, 2)]
        public void TestLineCounter(string fileName, int horizontal, int vertical)
        {
            Bitmap image = new Bitmap(fileName);

            image = image.GetBlobReady();

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            List<Control> controls = blobDetector.GetShapes();
            Bitmap shpaes = blobDetector.GetImage();
            shpaes.Save(@"D:/Data/test/outputs/blobs_" + fileName.Split('/').Last());

            LineCounter lineCounter = new LineCounter() { MinimumDistanceError = 5, MinimumPointsCount = 20 };
            lineCounter.ProcessEdges(controls[0].EdgePoints);

            Debug.WriteLine($"H lines:{lineCounter.HorizontalLines.Count}, V Lines: {lineCounter.VerticalLines.Count}");

            lineCounter.HorizontalLinesCount.ShouldEqual(horizontal);
            lineCounter.VerticalLinesCount.ShouldEqual(vertical);
        }

        [TestCase(@"D:/Data/test/inputs/button/test/test1.jpg", 2, 2)]
        [TestCase(@"D:\Data\test\inputs\password\test/test1.jpg", 2, 4)]
        [TestCase(@"D:\Data\test\inputs\combo\test/test1.jpg", 2, 2)]
        public void TestLineCounterImage(string fileName, int horizontal, int vertical)
        {
            Bitmap image = new Bitmap(fileName);

            image = image.GetBlobReady();

            LineCounter lineCounter = new LineCounter() { MinimumDistanceError = 5, MinimumPointsCount = 20 };
            lineCounter.ProcessImage(image);

            Debug.WriteLine($"H lines:{lineCounter.HorizontalLines.Count}, V Lines: {lineCounter.VerticalLines.Count}");

            lineCounter.HorizontalLinesCount.ShouldEqual(horizontal);
            lineCounter.VerticalLinesCount.ShouldEqual(vertical);
        }

        /// <summary>
        /// Tests the horizontal line count.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="horizontal">The horizontal.</param>
        [TestCase(@"D:/Data/test/inputs/button/test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\password\test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\combo\test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\cropped.jpg", 2)]
        public void TestHorizontalLineCount(string fileName, int horizontal)
        {
            int hCount;
            Bitmap image = new Bitmap(fileName);

            image = image.GetBlobReady();
            image.ToBinary();

            image.GetHorizontalLinesCount(out hCount);
            Debug.WriteLine($"hCount : {hCount}");
            hCount.ShouldEqual(horizontal);
        }

        /// <summary>
        /// Tests the horizontal line count.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="count">The count.</param>
        [TestCase(@"D:/Data/test/inputs/button/test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\password\test/test1.jpg", 4)]
        [TestCase(@"D:\Data\test\inputs\combo\test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\cropped.jpg", 4)]
        public void TestVerticalLineCount(string fileName, int count)
        {
            int vCount;
            Bitmap image = new Bitmap(fileName);
            image = image.GetBlobReady();
            image.ToBinary();
            image.GetVerticalLinesCount(out vCount);
            Debug.WriteLine($"vCount : {vCount}");
            vCount.ShouldEqual(count);
        }

        /// <summary>
        /// Tests the horizontal line count.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="count">The count.</param>
        [TestCase(@"D:/Data/test/inputs/button/test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\password\test/test1.jpg", 5)]
        [TestCase(@"D:\Data\test\inputs\combo\test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\checkbox\test/test1.jpg", 2)]
        public void TestVerticalLineCountBlobs(string fileName, int count)
        {
            int vCount;
            Bitmap image = new Bitmap(fileName);
            image = image.GetBlobReady();
            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            List<Control> controls = blobDetector.GetShapes();
            image = image.Crop(controls.Where(c => c.Width > 50 && c.Height > 50).ToList()[0].EdgePoints);

            image.ToBinary();

            image.GetVerticalLinesCount(out vCount);

            Debug.WriteLine($"vCount : {vCount}");

            vCount.ShouldEqual(count);
        }

        /// <summary>
        /// Tests the horizontal line count.
        /// </summary>
        /// <param name="fileName">
        /// Name of the file.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        [TestCase(@"D:/Data/test/inputs/button/test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\password\test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\combo\test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\checkbox\test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\checkbox\test/test2.jpg", 2)]
        public void TestHorizontalLineCountBlobs(string fileName, int count)
        {
            int vCount;
            Bitmap image = new Bitmap(fileName);
            image = image.GetBlobReady();
            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            List<Control> controls = blobDetector.GetShapes();
            image = image.Crop(controls.Where(c => c.Width > 50 && c.Height > 50).ToList()[0].EdgePoints);

            image.ToBinary();

            image.GetHorizontalLinesCount(out vCount);

            Debug.WriteLine($"hCount : {vCount}");

            vCount.ShouldEqual(count);
        }

        /// <summary>
        /// Tests the horizontal line count.
        /// </summary>
        /// <param name="fileName">
        /// Name of the file.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        [TestCase(@"D:/Data/test/inputs/button/test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\password\test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\combo\test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\checkbox\test/test1.jpg", 3)]
        [TestCase(@"D:\Data\test\inputs\checkbox\test/test2.jpg", 3)]
        [TestCase(@"D:\Data\test\inputs\date\test\test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\hr\test\test2.jpg", 1)]
        [TestCase(@"D:\Data\test\inputs\iframe\test\test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\image\test\test2.jpg", 4)]
        [TestCase(@"D:\Data\test\inputs\label\test\test1.jpg", 3)]
        [TestCase(@"D:\Data\test\inputs\link\test\test1.jpg", 3)]
        [TestCase(@"D:\Data\test\inputs\paragraph\test\test1.jpg", 3)]
        [TestCase(@"D:\Data\test\inputs\radio\test\test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\text\test\test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\range\test\test1.jpg", 1)]
        public void TestHorizontalLineCountBlobsPrewitt(string fileName, int count)
        {
            int vCount;
            Bitmap image = new Bitmap(fileName);

            image = image.GetBlobReady();

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            List<Control> controls = blobDetector.GetShapes();
            image = image.Crop(controls.Where(c => c.Width > 50 && c.Height > 50).ToList()[0].EdgePoints);

            image.ConvolutionFilter(FilterMatrix.Prewitt3x3Horizontal);

            image.ToBinary();

            image.GetHorizontalLinesCount(out vCount);

            Debug.WriteLine($"hCount : {vCount}");

            vCount.ShouldEqual(count);
        }

        /// <summary>
        /// Tests the horizontal line count.
        /// </summary>
        /// <param name="fileName">
        /// Name of the file.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        [TestCase(@"D:/Data/test/inputs/button/test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\password\test/test1.jpg", 5)]
        [TestCase(@"D:\Data\test\inputs\combo\test/test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\checkbox\test/test1.jpg", 3)]
        [TestCase(@"D:\Data\test\inputs\checkbox\test/test2.jpg", 3)]
        [TestCase(@"D:\Data\test\inputs\date\test\test1.jpg", 4)]
        [TestCase(@"D:\Data\test\inputs\date\test\test2.jpg", 4)]
        [TestCase(@"D:\Data\test\inputs\hr\test\test2.jpg", 1)]
        [TestCase(@"D:\Data\test\inputs\iframe\test\test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\image\test\test2.jpg", 4)]
        [TestCase(@"D:\Data\test\inputs\label\test\test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\link\test\test1.jpg", 3)]
        [TestCase(@"D:\Data\test\inputs\paragraph\test\test1.jpg", 3)]
        [TestCase(@"D:\Data\test\inputs\range\test\test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\range\range2_03.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\radio\test\test1.jpg", 2)]
        [TestCase(@"D:\Data\test\inputs\text\test\test1.jpg", 3)]
        public void TestVerticalLineCountBlobsPrewitt(string fileName, int count)
        {
            int vCount;
            Bitmap image = new Bitmap(fileName);

            image = image.GetBlobReady();

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            List<Control> controls = blobDetector.GetShapes();
            image = image.Crop(controls.Where(c => c.Width > 50 && c.Height > 50).ToList()[0].EdgePoints);

            image.ConvolutionFilter(FilterMatrix.Prewitt3x3Vertical);

            image.ToBinary();

            image.GetVerticalLinesCount(out vCount);

            Debug.WriteLine($"vCount : {vCount}");

            vCount.ShouldEqual(count);
        }

        /// <summary>
        /// Tests the horizontal count accuracy.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <param name="count">The count.</param>
        [TestCase(@"D:/Data/test/inputs/button", 2)]
        [TestCase(@"D:\Data\test\inputs\password", 2)]
        [TestCase(@"D:\Data\test\inputs\combo", 2)]
        [TestCase(@"D:\Data\test\inputs\checkbox", 3)]
        [TestCase(@"D:\Data\test\inputs\date", 2)]
        [TestCase(@"D:\Data\test\inputs\hr", 1)]
        [TestCase(@"D:\Data\test\inputs\iframe", 2)]
        [TestCase(@"D:\Data\test\inputs\image", 4)]
        [TestCase(@"D:\Data\test\inputs\label", 3)]
        [TestCase(@"D:\Data\test\inputs\link", 3)]
        [TestCase(@"D:\Data\test\inputs\paragraph", 3)]
        [TestCase(@"D:\Data\test\inputs\radio", 2)]
        [TestCase(@"D:\Data\test\inputs\text", 2)]
        [TestCase(@"D:\Data\test\inputs\range", 1)]
        public void TestHorizontalCountAccuracy(string directoryName, int count )
        {
            string[] images = Directory.GetFiles(directoryName);
            foreach (string image in images)
            {
                int acctual = GetHorizontalLineCount(image);
                acctual.ShouldEqual(count);
            }
        }

        /// <summary>
        /// Tests the horizontal count accuracy.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <param name="count">The count.</param>
        [TestCase(@"D:/Data/test/inputs/button", 2)]
        [TestCase(@"D:\Data\test\inputs\password", 5)]
        [TestCase(@"D:\Data\test\inputs\combo", 2)]
        [TestCase(@"D:\Data\test\inputs\checkbox", 3)]
        [TestCase(@"D:\Data\test\inputs\date", 4)]
        [TestCase(@"D:\Data\test\inputs\hr", 2)]
        [TestCase(@"D:\Data\test\inputs\iframe", 2)]
        [TestCase(@"D:\Data\test\inputs\image", 4)]
        [TestCase(@"D:\Data\test\inputs\label", 2)]
        [TestCase(@"D:\Data\test\inputs\link", 3)]
        [TestCase(@"D:\Data\test\inputs\paragraph", 3)]
        [TestCase(@"D:\Data\test\inputs\range", 2)]
        [TestCase(@"D:\Data\test\inputs\radio", 2)]
        [TestCase(@"D:\Data\test\inputs\text", 3)]
        public void TestVerticalCountAccuracy(string directoryName, int count)
        {
            string[] images = Directory.GetFiles(directoryName);
            foreach (string image in images)
            {
                int acctual = this.GetVerticalLineCount(image);
                acctual.ShouldEqual(count);
            }
        }

        /// <summary>
        /// Tests the horizontal count accuracy.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <param name="count">The count.</param>
        [TestCase(@"D:/Data/test/inputs/button", 2)]
        [TestCase(@"D:\Data\test\inputs\password", 2)]
        [TestCase(@"D:\Data\test\inputs\combo", 2)]
        [TestCase(@"D:\Data\test\inputs\checkbox", 3)]
        [TestCase(@"D:\Data\test\inputs\date", 2)]
        [TestCase(@"D:\Data\test\inputs\hr", 1)]
        [TestCase(@"D:\Data\test\inputs\iframe", 2)]
        [TestCase(@"D:\Data\test\inputs\image", 4)]
        [TestCase(@"D:\Data\test\inputs\label", 3)]
        [TestCase(@"D:\Data\test\inputs\link", 3)]
        [TestCase(@"D:\Data\test\inputs\paragraph", 3)]
        [TestCase(@"D:\Data\test\inputs\radio", 2)]
        [TestCase(@"D:\Data\test\inputs\text", 2)]
        [TestCase(@"D:\Data\test\inputs\range", 1)]
        public void TestHorizontalCountAccuracyPercnentage(string directoryName, int count)
        {
            string[] images = Directory.GetFiles(directoryName);
            int index = 0;
            int passCount = 0;
            foreach (string image in images)
            {
                int acctual = GetHorizontalLineCount(image);
                if (acctual == count)
                {
                    passCount++;
                }

                index++;
            }

            double percentage = ((passCount / (index * 1.0)) * 100);
            Debug.WriteLine($"accuracy : {percentage}");
            percentage.ShouldBeGreaterThan(80);
        }

        /// <summary>
        /// Tests the horizontal count accuracy.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <param name="count">The count.</param>
        [TestCase(@"D:/Data/test/inputs/button", 2)]
        [TestCase(@"D:\Data\test\inputs\password", 5)]
        [TestCase(@"D:\Data\test\inputs\combo", 2)]
        [TestCase(@"D:\Data\test\inputs\checkbox", 3)]
        [TestCase(@"D:\Data\test\inputs\date", 4)]
        [TestCase(@"D:\Data\test\inputs\hr", 2)]
        [TestCase(@"D:\Data\test\inputs\iframe", 2)]
        [TestCase(@"D:\Data\test\inputs\image", 4)]
        [TestCase(@"D:\Data\test\inputs\label", 2)]
        [TestCase(@"D:\Data\test\inputs\link", 3)]
        [TestCase(@"D:\Data\test\inputs\paragraph", 3)]
        [TestCase(@"D:\Data\test\inputs\range", 1)]
        [TestCase(@"D:\Data\test\inputs\radio", 2)]
        [TestCase(@"D:\Data\test\inputs\text", 3)]
        public void TestVerticalCountAccuracyPercnentage(string directoryName, int count)
        {
            string[] images = Directory.GetFiles(directoryName);
            int index = 0;
            int passCount = 0;
            foreach (string image in images)
            {
                int acctual = this.GetVerticalLineCount(image);
                if (acctual == count)
                {
                    passCount++;
                }

                index++;
            }

            double percentage = ((passCount / (index * 1.0)) * 100);
            Debug.WriteLine($"accuracy : {percentage}");
            percentage.ShouldBeGreaterThan(80);
        }
    }
}
