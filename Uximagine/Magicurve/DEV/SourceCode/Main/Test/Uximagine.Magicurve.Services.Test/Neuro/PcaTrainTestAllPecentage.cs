#region Imports

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using Accord.Math;
using AForge.Imaging.Filters;
using NUnit.Framework;
using Should;
using Uximagine.Magicurve.Core.Models;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing.Detectors;
using Uximagine.Magicurve.Image.Processing.Helpers;
using Uximagine.Magicurve.Neuro.Processing;
#endregion

namespace Uximagine.Magicurve.Services.Test.Neuro
{
    /// <summary>
    ///     Neural network training tests.
    /// </summary>
    [TestFixture]
    public class PcaTrainTestsAllPercentage
    {
        private List<Tuple<Bitmap, int>> images;

        private const int SampleSize = 32;

        private PcaClassifier classifier;

        private const int MinSize = 50;

        [TestFixtureSetUp]
        public void Setup()
        {
            this.images = new List<Tuple<Bitmap, int>>();
            Debug.WriteLine("Test Started");
            GetInputsOutputs(MinSize);

            this.classifier = PcaClassifier.GetInstance();
            this.classifier.TrainMachine(this.images, 0);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Debug.WriteLine("Test finished.");
        }

        [TestCase(@"D:/Data/test/inputs/button", ControlType.Button)]
        [TestCase(@"D:/Data/test/inputs/text", ControlType.InputText)]
        [TestCase(@"D:/Data/test/inputs/combo", ControlType.ComboBox)]
        [TestCase(@"D:/Data/test/inputs/password", ControlType.InputPassword)]
        [TestCase(@"D:/Data/test/inputs/paragraph", ControlType.Paragraph)]
        [TestCase(@"D:/Data/test/inputs/label", ControlType.Label)]
        [TestCase(@"D:/Data/test/inputs/iframe", ControlType.Iframe)]
        [TestCase(@"D:/Data/test/inputs/hr", ControlType.HLine)]
        [TestCase(@"D:/Data/test/inputs/range", ControlType.Range)]
        [TestCase(@"D:/Data/test/inputs/checkbox", ControlType.CheckBox)]
        [TestCase(@"D:/Data/test/inputs/date", ControlType.DatePicker)]
        [TestCase(@"D:/Data/test/inputs/link", ControlType.HyperLink)]
        [TestCase(@"D:/Data/test/inputs/image", ControlType.Image)]
        [TestCase(@"D:/Data/test/inputs/radio", ControlType.RadioButton)]
        public void TestButton(string directory, ControlType type)
        {
            string[] files = Directory.GetFiles(directory);

            int index = 0;
            int passCount = 0;

            foreach (var image in files)
            {
                Bitmap testInputButton = GetInputVector(image, MinSize);
                var decision = this.classifier.Compute(testInputButton);
                Debug.WriteLine($"expected {type} but actual {(ControlType)(decision + 1)} ");

                if (decision == type.To<int>() - 1)
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
        /// Gets the inputs outputs.
        /// </summary>
        /// <param name="minSize">The minimum size.</param>
        private void GetInputsOutputs(int minSize)
        {
            AddSymbols(@"D:/Data/test/inputs/button", ControlType.Button.To<int>() - 1, minSize);
            AddSymbols(@"D:/Data/test/inputs/combo", ControlType.ComboBox.To<int>() - 1, minSize);
            AddSymbols(@"D:/Data/test/inputs/paragraph", ControlType.Paragraph.To<int>() - 1, minSize);
            AddSymbols(@"D:/Data/test/inputs/text", ControlType.InputText.To<int>() - 1, minSize);
            AddSymbols(@"D:/Data/test/inputs/radio", ControlType.RadioButton.To<int>() - 1, minSize);
            AddSymbols(@"D:/Data/test/inputs/image", ControlType.Image.To<int>() - 1, minSize);
            AddSymbols(@"D:/Data/test/inputs/password", ControlType.InputPassword.To<int>() - 1, minSize);
            AddSymbols(@"D:/Data/test/inputs/checkbox", ControlType.CheckBox.To<int>() - 1, minSize);
            AddSymbols(@"D:/Data/test/inputs/date", ControlType.DatePicker.To<int>() - 1, minSize);
            AddSymbols(@"D:/Data/test/inputs/label", ControlType.Label.To<int>() - 1, minSize);
            AddSymbols(@"D:/Data/test/inputs/iframe", ControlType.Iframe.To<int>() - 1, minSize);
            AddSymbols(@"D:/Data/test/inputs/hr", ControlType.HLine.To<int>() - 1, minSize);
            AddSymbols(@"D:/Data/test/inputs/range", ControlType.Range.To<int>() - 1, minSize);
            AddSymbols(@"D:/Data/test/inputs/link", ControlType.HyperLink.To<int>() - 1, minSize);
        }

        /// <summary>
        /// Adds the symbols.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="label">The label.</param>
        /// <param name="minSize">The minimum size.</param>
        private void AddSymbols(string folder, int label, int minSize)
        {
            string[] files = Directory.GetFiles(folder);
            var samples = files.Length;
            for (var i = 0; i < samples; i++)
            {
                Bitmap cropped = Crop(files[i], minSize);
                if (cropped != null)
                {
                    this.images.Add(new Tuple<Bitmap, int>(cropped, label));
                }
            }
        }

        /// <summary>
        /// Gets the input vector.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="minSize">The minimum size.</param>
        /// <returns></returns>
        public Bitmap GetInputVector(string fileName, int minSize)
        {
            Bitmap cropped = Crop(fileName, minSize);

            return cropped;
        }


        /// <summary>
        /// Crops the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="minSize">The minimum size.</param>
        /// <returns></returns>
        private Bitmap Crop(string fileName, int minSize)
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
            if (controls.Count(t => t.Width > minSize && t.Height > minSize) == 0)
            {
                return null;
            }

            Control control = controls.Where(t => t.Width > minSize && t.Height > minSize).ToList()[0];

            Bitmap cropped = image.Vectorize(control.EdgePoints, 1);

            return cropped;
        }
    }
}