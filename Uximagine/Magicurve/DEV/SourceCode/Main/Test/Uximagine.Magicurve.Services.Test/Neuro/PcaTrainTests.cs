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
    public class PcaTrainTests
    {
        private List<Tuple<Bitmap, int>> _images;

        private static int _sampleSize = 32;

        [TestFixtureSetUp]
        public void Setup()
        {
            _images = new List<Tuple<Bitmap, int>>();
            Debug.WriteLine("Test Started");
        }

        public void TearDown()
        {
            Debug.WriteLine("Test Started");
        }

        [TestCase(50, 32)]
        [TestCase(50, 40)]
        [TestCase(75, 32)]
        [TestCase(75, 40)]
        [TestCase(100, 32)]
        [TestCase(100, 40)]
        public void Train(int minSize, int sampleSize)
        {
            _sampleSize = sampleSize;

            GetInputsOutputs(minSize);

            PcaClassifier classifier = PcaClassifier.GetInstance();
            classifier.TrainMachine(_images);

            Bitmap testInputButton = GetInputVector(@"D:/Data/test/inputs/button/test/button_07.jpg", minSize);
            Bitmap testInputCombo = GetInputVector(@"D:/Data/test/inputs/combo/test/combo2_01.jpg", minSize);
            Bitmap testInputParah = GetInputVector(@"D:/Data/test/inputs/paragraph/test/parah2_06.jpg", minSize);
            Bitmap testInputtext= GetInputVector(@"D:/Data/test/inputs/text/test/text_11.jpg", minSize);
            Bitmap radio = GetInputVector(@"D:/Data/test/inputs/radio/test/radio_11.jpg", minSize);
            Bitmap image = GetInputVector(@"D:/Data/test/inputs/image/test/image2_05.jpg", minSize);
            Bitmap image2 = GetInputVector(@"D:/Data/test/inputs/image/test/image_13.jpg", minSize);
            Bitmap password = GetInputVector(@"D:/Data/test/inputs/password/test/password2_03.jpg", minSize);

            var decision = classifier.Compute(testInputButton);
            decision.ShouldEqual(ControlType.Button.To<int>() - 1);

            decision = classifier.Compute(testInputCombo);
            decision.ShouldEqual(ControlType.ComboBox.To<int>() - 1);

            decision = classifier.Compute(testInputParah);
            decision.ShouldEqual(ControlType.Paragraph.To<int>() - 1);

            decision = classifier.Compute(testInputtext);
            decision.ShouldEqual(ControlType.InputText.To<int>() - 1);

            decision = classifier.Compute(radio);
            decision.ShouldEqual(ControlType.RadioButton.To<int>() - 1);

            decision = classifier.Compute(image);
            //decision.ShouldEqual(ControlType.Image.To<int>() - 1);

            decision = classifier.Compute(image2);
            decision.ShouldEqual(ControlType.Image.To<int>() - 1);

            decision = classifier.Compute(password);
            decision.ShouldEqual(ControlType.InputPassword.To<int>() - 1);
        }

        /// <summary>
        /// Gets the inputs outputs.
        /// </summary>
        /// <param name="minSize">The minimum size.</param>
        private void GetInputsOutputs(int minSize)
        {
          AddSymbols(@"D:/Data/test/inputs/button", 0, minSize);
          AddSymbols(@"D:/Data/test/inputs/combo", 1, minSize);
          AddSymbols(@"D:/Data/test/inputs/paragraph", 2, minSize);
          AddSymbols(@"D:/Data/test/inputs/text", 3, minSize);
          AddSymbols(@"D:/Data/test/inputs/radio", 4, minSize);
          AddSymbols(@"D:/Data/test/inputs/image", 5, minSize);
          AddSymbols(@"D:/Data/test/inputs/password", 6, minSize);
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

            cropped = cropped.Resize(_sampleSize, _sampleSize);

            return cropped;
        }
    }
}