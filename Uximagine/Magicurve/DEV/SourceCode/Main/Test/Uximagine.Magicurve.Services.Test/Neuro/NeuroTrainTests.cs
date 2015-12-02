#region Imports
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using Accord.Imaging;
using Accord.Imaging.Filters;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using NUnit.Framework;
using Should;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing.Detectors;
using Uximagine.Magicurve.Image.Processing.Helpers;
using Uximagine.Magicurve.Neuro.Processing; 
#endregion

namespace Uximagine.Magicurve.Services.Test.Neuro
{
    /// <summary>
    ///     Nural network training tests.
    /// </summary>
    [TestFixture]
    public class NeuroTrainTests
    {
        private static int _index;

        [TestCase(50)]
        [TestCase(75)]
        [TestCase(100)]
        public void Train(int minSize)
        {
            double[][] inputs;
            int[] outputs;
            GetInputsOutputs(minSize, out inputs, out outputs);

            IClassifer classifier = ShapeClassfier.GetInstance(3, 2);
            classifier.TrainMachine(inputs, outputs);

            double[] testInputButton = GetInputVector(@"D:/Data/test/inputs/button/test/button_07.jpg", minSize, 4.3f);
            double[] testInputCombo = GetInputVector(@"D:/Data/test/inputs/combo/test/combo2_01.jpg", minSize, 4.3f);
            var decision = classifier.Compute(testInputButton);
            decision.ShouldEqual(0);

            decision = classifier.Compute(testInputCombo);
            decision.ShouldEqual(1);
        }

        private void GetInputsOutputs(int minSize, out double[][] inputs, out int[] outputs)
        {
            string[] buttons = Directory.GetFiles(@"D:/Data/test/inputs/button");
            var samples = buttons.Length;

            string[] combos = Directory.GetFiles(@"D:/Data/test/inputs/combo");
            samples += combos.Length;

            inputs = new double[samples][];
            outputs = new int[samples];

            for (var i = 0; i < buttons.Length; i++)
            {
                double[] input = GetInputVector(buttons[i], minSize, 4.3f);
                inputs[i] = input;
                outputs[i] = 0;
                Debug.WriteLine("({0}, {1}, {2}) - {3}",
                    inputs[i][0], inputs[i][1], inputs[i][2], outputs[i]);
            }

            var offset = buttons.Length;

            for (var i = 0; i < combos.Length; i++)
            {
                double[] input = GetInputVector(combos[i], minSize, 4.3f);
                inputs[offset + i] = input;
                outputs[offset + i] = 1;
                Debug.WriteLine("({0}, {1}, {2}) - {3}",
                    inputs[offset + i][0], inputs[offset + i][1], inputs[offset + i][2], outputs[offset + i]);
            }
        }

        public double[] GetInputVector(string fileName, int minSize, float multiflier)
        {
            Bitmap cropped = Crop(fileName, minSize);

            int hLinesCount;
            int vLinesCount;
            GetLinesCount(cropped, out hLinesCount, out vLinesCount);

            var cornersCount = GetFastCornersCount(40, cropped);

            return new double[] {cornersCount, hLinesCount, vLinesCount};
        }

        /// <summary>
        /// Gets the corners count.
        /// </summary>
        /// <param name="multiflier">The multiflier.</param>
        /// <param name="cropped">The cropped.</param>
        /// <returns>
        /// The corner count.
        /// </returns>
        public static int GetCornersCount(float multiflier, Bitmap cropped)
        {
            // create corners detector's instance
            HarrisCornersDetector hcd = new HarrisCornersDetector();
            //hcd.Threshold = hcd.Threshold*multiflier;
            hcd.K = hcd.K*multiflier;
            List<IntPoint> corners = hcd.ProcessImage(cropped);
            CornersMarker filter = new CornersMarker(hcd, Color.Red);
            // apply the filter
            Bitmap image = filter.Apply(cropped);

            image.Save(@"D:/Data/test/outputs/h_corners_" + _index++ + ".jpg");
            var cornersCount = corners.Count;
            return cornersCount;
        }

        private static int GetFastCornersCount(int threshold, Bitmap cropped)
        {
            // Create a new FAST Corners Detector
            FastCornersDetector fast = new FastCornersDetector
            {
                Suppress = true, // suppress non-maximum points
                Threshold = threshold // less leads to more corners
            };

            // Process the image looking for corners
            List<IntPoint> points = fast.ProcessImage(cropped);

            // Create a filter to mark the corners
            PointsMarker marker = new PointsMarker(points, Color.Red);

            // Apply the corner-marking filter
            Bitmap markers = marker.Apply(cropped);

            markers.Save(@"D:/Data/test/outputs/f_corners_" + _index++ + ".jpg");
            var cornersCount = points.Count;
            return cornersCount;
        }

        private static void GetLinesCount(Bitmap cropped, out int hLinesCount, out int vLinesCount)
        {
            Bitmap horizontal = ((Bitmap) cropped.Clone()).HorizontalEdges();
            Bitmap vertical = ((Bitmap) cropped.Clone()).VerticalEdges();

            horizontal = Grayscale.CommonAlgorithms.BT709.Apply(horizontal);
            vertical = Grayscale.CommonAlgorithms.BT709.Apply(vertical);

            HoughLineTransformation hlt = new HoughLineTransformation();
            hlt.ProcessImage(horizontal);
            hLinesCount = hlt.GetLinesByRelativeIntensity(.5).Count();

            hlt.ProcessImage(vertical);
            vLinesCount = hlt.GetLinesByRelativeIntensity(.5).Count();
        }

        private static Bitmap Crop(string fileName, int minSize)
        {
            Bitmap image = new Bitmap(fileName); // Lena's picture

            image = Grayscale.CommonAlgorithms.BT709.Apply(image);

            Threshold threshold = new Threshold();
            threshold.ApplyInPlace(image);

            Median median = new Median();
            median.ApplyInPlace(image);

            Invert invert = new Invert();
            invert.ApplyInPlace(image);

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            List<Control> controls = blobDetector.GetShapes();
            Control control = controls.Where(t => t.Width > minSize && t.Height > minSize).ToList()[0];
            //Debug.WriteLine("{0} {1}", control.Width, control.Height);

            Bitmap cropped = image.Crop(control.EdgePoints);
            return cropped;
        }
    }
}