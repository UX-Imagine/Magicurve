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
    public class PcaTrainTests2
    {
        private List<Tuple<Bitmap, int>> _images;

        private const int SampleSize = 32;

        private PcaClassifier _classifier;

        private const int MinSize = 50;

        [TestFixtureSetUp]
        public void Setup()
        {
            _images = new List<Tuple<Bitmap, int>>();
            Debug.WriteLine("Test Started");
            GetInputsOutputs(50);

            _classifier = PcaClassifier.GetInstance();
            _classifier.TrainMachine(_images);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Debug.WriteLine("Test finished.");
        }

        [TestCase(@"D:/Data/test/inputs/button/test/button_07.jpg")]
        [TestCase(@"D:/Data/test/inputs/button/test/button_07.jpg")]
        public void TestButton(string fileName)
        {
            Bitmap testInputButton = GetInputVector(fileName, MinSize);
            var decision = _classifier.Compute(testInputButton);
            decision.ShouldEqual(ControlType.Button.To<int>() - 1);
        }

        [TestCase(@"D:/Data/test/inputs/combo/test/combo2_01.jpg")]
        public void TestCombo(string fileName)
        {
            Bitmap testInputButton = GetInputVector(fileName, MinSize);
            var decision = _classifier.Compute(testInputButton);
            decision.ShouldEqual(ControlType.ComboBox.To<int>() - 1);
        }

        [TestCase(@"D:/Data/test/inputs/paragraph/test/parah2_06.jpg")]
        public void TestParah(string fileName)
        {
            Bitmap testInputButton = GetInputVector(fileName, MinSize);
            var decision = _classifier.Compute(testInputButton);
            decision.ShouldEqual(ControlType.Paragraph.To<int>() - 1);
        }


        [TestCase(@"D:/Data/test/inputs/text/test/text_11.jpg")]
        public void TestText(string fileName)
        {
            Bitmap testInputButton = GetInputVector(fileName, MinSize);
            var decision = _classifier.Compute(testInputButton);
            decision.ShouldEqual(ControlType.InputText.To<int>() - 1);
        }

        [TestCase(@"D:/Data/test/inputs/radio/test/radio_11.jpg")]
        public void TestRadio(string fileName)
        {
            Bitmap testInputButton = GetInputVector(fileName, MinSize);
            var decision = _classifier.Compute(testInputButton);
            decision.ShouldEqual(ControlType.RadioButton.To<int>() - 1);
        }

        [TestCase(@"D:/Data/test/inputs/image/test/image_13.jpg")]
        [TestCase(@"D:/Data/test/inputs/image/test/image2_08.jpg")]
        public void TestImage(string fileName)
        {
            Bitmap testInputButton = GetInputVector(fileName, MinSize);
            var decision = _classifier.Compute(testInputButton);
            decision.ShouldEqual(ControlType.Image.To<int>() - 1);
        }

        [TestCase(@"D:/Data/test/inputs/password/test/password2_03.jpg")]
        public void TestPassword(string fileName)
        {
            Bitmap testInputButton = GetInputVector(fileName, MinSize);
            var decision = _classifier.Compute(testInputButton);
            decision.ShouldEqual(ControlType.InputPassword.To<int>() - 1);
        }

        [TestCase(@"D:/Data/test/inputs/checkbox/test/check_02.jpg")]
        [TestCase(@"D:/Data/test/inputs/checkbox/test/check_13.jpg")]
        public void TestCheckBox(string fileName)
        {
            Bitmap testInputButton = GetInputVector(fileName, MinSize);
            var decision = _classifier.Compute(testInputButton);
            decision.ShouldEqual(ControlType.CheckBox.To<int>() - 1);
        }

        [TestCase(@"D:/Data/test/inputs/date/test/pick_06.jpg")]
        [TestCase(@"D:/Data/test/inputs/date/test/pick_12.jpg")]
        public void TestDatePicker(string fileName)
        {
            Bitmap vector = GetInputVector(fileName, MinSize);
            var decision = _classifier.Compute(vector);
            decision.ShouldEqual(ControlType.DatePicker.To<int>() - 1);
        }

        [TestCase(@"D:/Data/test/inputs/label/test/area2_16.jpg")]
        public void TestLabel(string fileName)
        {
            Bitmap vector = GetInputVector(fileName, MinSize);
            var decision = _classifier.Compute(vector);
            decision.ShouldEqual(ControlType.Label.To<int>() - 1);
        }

        [TestCase(@"D:/Data/test/inputs/link/test/link2_06.jpg")]
        public void TestHyperLink(string fileName)
        {
            Bitmap vector = GetInputVector(fileName, MinSize);
            var decision = _classifier.Compute(vector);
            decision.ShouldEqual(ControlType.HyperLink.To<int>() - 1,
                $"expected {(ControlType)(ControlType.HyperLink.To<int>())} but actual {(ControlType) decision + 1} ");
        }

        [TestCase(@"D:/Data/test/inputs/iframe/test/frame_14.jpg")]
        [TestCase(@"D:/Data/test/inputs/iframe/test/frame2_01.jpg")]
        public void TestIFrame(string fileName)
        {
            Bitmap vector = GetInputVector(fileName, MinSize);
            var decision = _classifier.Compute(vector);
            decision.ShouldEqual(ControlType.Iframe.To<int>() - 1);
        }

        [TestCase(@"D:/Data/test/inputs/hr/test/line_10.jpg")]
        [TestCase(@"D:/Data/test/inputs/hr/test/line_13.jpg")]
        public void TestHr(string fileName)
        {
            Bitmap vector = GetInputVector(fileName, MinSize);
            var decision = _classifier.Compute(vector);
            decision.ShouldEqual(ControlType.HLine.To<int>() - 1);
        }

        [TestCase(@"D:/Data/test/inputs/range/test/range2_12.jpg")]
        public void TestRange(string fileName)
        {
            Bitmap vector = GetInputVector(fileName, MinSize);
            var decision = _classifier.Compute(vector);
            decision.ShouldEqual(ControlType.Range.To<int>() - 1);
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
          AddSymbols(@"D:/Data/test/inputs/link", ControlType.HyperLink.To<int>() - 1, minSize);
          AddSymbols(@"D:/Data/test/inputs/iframe", ControlType.Iframe.To<int>() - 1, minSize);
          AddSymbols(@"D:/Data/test/inputs/hr", ControlType.HLine.To<int>() - 1, minSize);
          AddSymbols(@"D:/Data/test/inputs/range", ControlType.Range.To<int>() - 1, minSize);
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
                    _images.Add(new Tuple<Bitmap, int>(cropped, label));
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
           
            Bitmap cropped = image.Crop(control.EdgePoints);

            cropped = cropped.Resize(SampleSize, SampleSize);

            return cropped;
        }
    }
}