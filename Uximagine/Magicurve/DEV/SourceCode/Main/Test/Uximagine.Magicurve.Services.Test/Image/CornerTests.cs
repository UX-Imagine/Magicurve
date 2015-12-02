using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using NUnit.Framework;
using Should;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing.Detectors;
using Uximagine.Magicurve.Image.Processing.Helpers;
using Uximagine.Magicurve.Image.Processing.ShapeCheckers;

namespace Uximagine.Magicurve.Services.Test.Image
{
    /// <summary>
    ///     Test the corner detect algorithms
    /// </summary>
    [TestFixture]
    public class CornerTests
    {
        [TestCase(@"D:/Data/test/inputs/combo_10.jpg", 6)]
        public void TestSusan(string fileName, int cornerCount)
        {
            Bitmap image = new Bitmap(fileName); // Lena's picture

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            image.Save(@"D:/Data/test/outputs/grayscale.jpg");

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            image.Save(@"D:/Data/test/outputs/threshold.jpg");

            Median median = new Median();
            median.ApplyInPlace(image);

            //var correctFormatImage = image.ConvertToFormat(PixelFormat.Format24bppRgb);

            Invert invert = new Invert();
            invert.ApplyInPlace(image);
            image.Save(@"D:/Data/test/outputs/invert.jpg");

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);
            List<Control> shapes = blobDetector.GetShapes();

            foreach (Control control in shapes)
            {
                Bitmap shape = control.EdgePoints.ConvertToBitmap();
                // create corner detector's instance
                SusanCornersDetector scd = new SusanCornersDetector();
                // create corner maker filter
                CornersMarker filter = new CornersMarker(scd, Color.Red);
                // apply the filter
                filter.ApplyInPlace(shape);
                shape.Save(@"D:/Data/test/outputs/corners.jpg");

                List<IntPoint> corners = scd.ProcessImage(shape);
                IShapeChecker shapeChecker = new UiShapeChecker();
                ControlType type = shapeChecker.GetControlType(corners);
                Debug.WriteLine(type);
                type.ShouldEqual(ControlType.ComboBox);
            }
        }
    }
}