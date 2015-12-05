using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using Uximagine.Magicurve.Core;
using Uximagine.Magicurve.Core.Diagnostics;
using Uximagine.Magicurve.Core.Diagnostics.Logging;
using Uximagine.Magicurve.Core.Shapes;
using Uximagine.Magicurve.Image.Processing.Detectors;
using Uximagine.Magicurve.Image.Processing.Helpers;
using Uximagine.Magicurve.Neuro.Processing;

namespace Uximagine.Magicurve.Image.Processing
{
    /// <summary>
    /// The classifier trainer.
    /// </summary>
    public class Trainer
    {
        /// <summary>
        /// Gets or sets a value indicating whether [force training].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [force training]; otherwise, <c>false</c>.
        /// </value>
        public bool ForceTraining { get; set; }

        /// <summary>
        /// Gets or sets the operations log.
        /// </summary>
        /// <value>
        /// The operations log.
        /// </value>
        private List<Operation> OperationsLog { get; set; }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        private List<Tuple<Bitmap, int>> Images
        {
            get;
            set;
        }

        /// <summary>
        /// The sample size
        /// </summary>
        private static int _sampleSize = 32;

        /// <summary>
        /// Initializes a new instance of the <see cref="Trainer"/> class.
        /// </summary>
        public Trainer()
        {
            Images = new List<Tuple<Bitmap, int>>();
            OperationsLog = new List<Operation>();
        }

        /// <summary>
        /// Trains the specified minimum size.
        /// </summary>
        /// <param name="minSize">
        /// The minimum size.
        /// </param>
        /// <param name="sampleSize">
        /// Size of the sample.
        /// </param>
        public void Train(int minSize, int sampleSize)
        {
            this.LogOperation("Training started");

            _sampleSize = Math.Max(sampleSize, _sampleSize);

            this.LogOperation("PCA Started");

            GetFeatures(minSize);

            this.LogOperation("PCA Completed");

            PcaClassifier classifier = PcaClassifier.GetInstance();

            if (classifier.IsTrained == false || ForceTraining)
            {
                classifier.TrainMachine(Images);
            }

            this.LogOperation("Training Completed");

            WriteOperationsLog();
        }

        /// <summary>
        /// Gets the inputs outputs.
        /// </summary>
        /// <param name="minSize">
        /// The minimum size.
        /// </param>
        private void GetFeatures(int minSize)
        {
            foreach (string key in ConfigurationData.TrainDataInfo.Keys)
            {
                string[] values = ConfigurationData.TrainDataInfo.GetValues(key);

                if (values == null) continue;

                foreach (string value in values)
                {
                    AddSymbols(value, int.Parse(key), minSize);
                }
            }
        }

        /// <summary>
        /// Adds the symbols.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="label">The label.</param>
        /// <param name="minSize">The minimum size.</param>
        private void AddSymbols(string folder, int label, int minSize)
        {
            if (folder != null)
            {
                string[] files = Directory.GetFiles(path: HostingEnvironment.MapPath(virtualPath: folder));
                //string[] files = Directory.GetFiles(folder);
                var samples = files.Length;

                for (var i = 0; i < samples; i++)
                {
                    Bitmap cropped = Crop(files[i], minSize);
                    if (cropped != null)
                    {
                        Images.Add(new Tuple<Bitmap, int>(cropped, label));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the input vector.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="minSize">The minimum size.</param>
        /// <returns>
        /// The input vector.
        /// </returns>
        public Bitmap GetInputVector(string fileName, int minSize)
        {
            Bitmap cropped = Crop(fileName, minSize);

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
        /// The cropped symbol.
        /// </returns>
        private static Bitmap Crop(string fileName, int minSize)
        {
            Bitmap image = new Bitmap(fileName); // Lena's picture

            image = image.GetBlobReady();

            IBlobDetector blobDetector = new HullBlobDetector();
            blobDetector.ProcessImage(image);

            List<Control> controls = blobDetector.GetShapes();

            Control control = controls.Where(t => t.Width > minSize && t.Height > minSize).ToList().FirstOrDefault();

            if (control != null)
            {
                Bitmap cropped = image.Crop(control.EdgePoints);

                cropped = cropped.Resize(_sampleSize, _sampleSize);

                return cropped;
            }

            return null;
        }

        /// <summary>
        /// Logs the operation.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        private void LogOperation(string message)
        {
            if (ConfigurationData.MustLogOperationalPerformance)
            {
                Operation operation = new Operation();

                operation.Timestamp = DateTimeHelper.Now;
                operation.Message = string.Format(
                    CultureInfo.InvariantCulture,
                    message,
                    this.GetType().Name);

                this.OperationsLog.Add(operation);
            }
        }

        /// <summary>
        /// Writes the operations log.
        /// </summary>
        private void WriteOperationsLog()
        {
            if (ConfigurationData.MustLogOperationalPerformance)
            {
                OperationsLog.ForEach(o =>
                {
                    LogManager.Log(
                    this.GetType(),
                    ErrorSeverity.Information,
                    string.Format("{0} at {1}", o.Message, o.Timestamp));
                });
            }
        }
    }
}
