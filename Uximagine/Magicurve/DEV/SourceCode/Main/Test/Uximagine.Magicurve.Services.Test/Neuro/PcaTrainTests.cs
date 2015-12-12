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
        /// <summary>
        /// The _sample size.
        /// </summary>
        private static int sampleSize = 32;

        /// <summary>
        /// The images.
        /// </summary>
        private List<Tuple<Bitmap, int>> images;

        /// <summary>
        /// The setup.
        /// </summary>
        [TestFixtureSetUp]
        public void Setup()
        {
            this.images = new List<Tuple<Bitmap, int>>();
            Debug.WriteLine("Test Started");
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        public void TearDown()
        {
            Debug.WriteLine("Test Started");
        }

        /// <summary>
        /// The train.
        /// </summary>
        /// <param name="minSize">
        /// The min size.
        /// </param>
        /// <param name="size">
        /// The sample size.
        /// </param>
        [TestCase(50, 32)]
        [TestCase(50, 40)]
        [TestCase(75, 32)]
        [TestCase(75, 40)]
        [TestCase(100, 32)]
        [TestCase(100, 40)]
        public void Train(int minSize, int size)
        {
            sampleSize = size;

            this.GetInputsOutputs(minSize);

            PcaClassifier classifier = PcaClassifier.GetInstance();
            classifier.TrainMachine(this.images, 0);

            Bitmap testInputButton = this.GetInputVector(@"D:/Data/test/inputs/button/test/test1.jpg", minSize);
            Bitmap testInputCombo = this.GetInputVector(@"D:/Data/test/inputs/combo/test/test1.jpg", minSize);
            Bitmap testInputParah = this.GetInputVector(@"D:/Data/test/inputs/paragraph/test/test1.jpg", minSize);
            Bitmap testInputtext = this.GetInputVector(@"D:/Data/test/inputs/text/test/test1.jpg", minSize);
            Bitmap radio = this.GetInputVector(@"D:/Data/test/inputs/radio/test/test1.jpg", minSize);
            Bitmap image = this.GetInputVector(@"D:/Data/test/inputs/image/test/test1.jpg", minSize);
            Bitmap image2 = this.GetInputVector(@"D:/Data/test/inputs/image/test/test2.jpg", minSize);
            Bitmap password = this.GetInputVector(@"D:/Data/test/inputs/password2/test/test1.jpg", minSize);

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
            decision.ShouldEqual(ControlType.Image.To<int>() - 1);

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
            this.AddSymbols(@"D:/Data/test/inputs/button", ControlType.Button.To<int>() - 1, minSize);
            this.AddSymbols(@"D:/Data/test/inputs/combo", ControlType.ComboBox.To<int>() - 1, minSize);
            this.AddSymbols(@"D:/Data/test/inputs/paragraph", ControlType.Paragraph.To<int>() - 1, minSize);
            this.AddSymbols(@"D:/Data/test/inputs/text", ControlType.InputText.To<int>() - 1, minSize);
            this.AddSymbols(@"D:/Data/test/inputs/radio", ControlType.RadioButton.To<int>() - 1, minSize);
            this.AddSymbols(@"D:/Data/test/inputs/image", ControlType.Image.To<int>() - 1, minSize);
            this.AddSymbols(@"D:/Data/test/inputs/password", ControlType.InputPassword.To<int>() - 1, minSize);
            this.AddSymbols(@"D:/Data/test/inputs/checkbox", ControlType.CheckBox.To<int>() - 1, minSize);
            this.AddSymbols(@"D:/Data/test/inputs/date", ControlType.DatePicker.To<int>() - 1, minSize);
            this.AddSymbols(@"D:/Data/test/inputs/label", ControlType.Label.To<int>() - 1, minSize);
            this.AddSymbols(@"D:/Data/test/inputs/iframe", ControlType.Iframe.To<int>() - 1, minSize);
            this.AddSymbols(@"D:/Data/test/inputs/hr", ControlType.HLine.To<int>() - 1, minSize);
            this.AddSymbols(@"D:/Data/test/inputs/range", ControlType.Range.To<int>() - 1, minSize);
            this.AddSymbols(@"D:/Data/test/inputs/link", ControlType.HyperLink.To<int>() - 1, minSize);
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
                Bitmap cropped = this.Crop(files[i], minSize);
                if (cropped != null)
                {
                    this.images.Add(new Tuple<Bitmap, int>(cropped, label));
                }
            }
        }

        /// <summary>
        /// Gets the input vector.
        /// </summary>
        /// <param name="fileName">
        /// Name of the file.
        /// </param>
        /// <param name="minSize">
        /// The minimum size.
        /// </param>
        /// <returns>
        /// The cropped image.
        /// </returns>
        public Bitmap GetInputVector(string fileName, int minSize)
        {
            Bitmap cropped = this.Crop(fileName, minSize);

            return cropped;
        }

        /// <summary>
        /// Crops the specified file name.
        /// </summary>
        /// <param name="fileName">
        /// Name of the file.
        /// </param>
        /// <param name="minSize">
        /// The minimum size.
        /// </param>
        /// <returns>
        /// The cropped image.
        /// </returns>
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

            cropped = cropped.Resize(sampleSize, sampleSize);

            return cropped;
        }
    }
}