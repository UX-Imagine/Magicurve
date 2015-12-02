using System;
using System.Collections.Generic;
using System.Drawing;
using Accord.Imaging.Converters;
using Accord.MachineLearning;
using Accord.Statistics.Analysis;

namespace Uximagine.Magicurve.Neuro.Processing
{
    /// <summary>
    /// Principal Component Analysis _classifier
    /// </summary>
    public class PcaClassifier : IClassifer
    {
        /// <summary>
        /// The analysis.
        /// </summary>
        private PrincipalComponentAnalysis _pca;

        /// <summary>
        /// The classifier.
        /// </summary>
        private MinimumMeanDistanceClassifier _classifier;

        /// <summary>
        /// The _instance.
        /// </summary>
        private static PcaClassifier _instance;

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        public List<Tuple<Bitmap, int>> Images
        {
            get; set;
        }

        /// <summary>
        /// Prevents a default _instance of the <see cref="PcaClassifier"/> class from being created.
        /// </summary>
        private PcaClassifier()
        {
        }

        /// <summary>
        /// Gets the _instance.
        /// </summary>
        /// <returns>
        /// The single _instance.
        /// </returns>
        public static PcaClassifier GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PcaClassifier();
            }

            return _instance;
        }

        /// <summary>
        /// Builds the pca.
        /// </summary>
        private void BuildPca()
        {
            // Extract feature vectors
            double[][] fearures = Extract();

            // Create a new Principal Component Analysis object
            _pca = new PrincipalComponentAnalysis(fearures, AnalysisMethod.Center);

            // Compute it
            _pca.Compute();
        }

        /// <summary>
        /// Trains the machine.
        /// </summary>
        public void TrainMachine(List<Tuple<Bitmap, int>> images)
        {
            this.Images = images;
            this.TrainMachine();
        }

        /// <summary>
        /// Trains the machine.
        /// </summary>
        public void TrainMachine()
        {
            if (Images == null)
            {
                throw new ArgumentException("Images cannot be null");
            }

            BuildPca();

            ImageToArray converter = new ImageToArray(min: -1, max: +1);

            int rows = this.Images.Count;
            double[][] inputs = new double[rows][];
            double[][] features = new double[rows][];
            int[] outputs = new int[rows];

            int index = 0;
            foreach (Tuple<Bitmap, int> tuple in this.Images)
            {
                Bitmap image = tuple.Item1;
                int label = tuple.Item2;

                double[] input;
                converter.Convert(image, out input);

                double[] feature = _pca.Transform(input);

                inputs[index] = input;
                features[index] = feature;
                outputs[index] = label;
                index++;
            }

            this.TrainMachine(features, outputs);
        }

        /// <summary>
        /// Trains the machine.
        /// </summary>
        /// <param name="features">The inputs.</param>
        /// <param name="outputs">The outputs.</param>
        public void TrainMachine(double[][] features, int[] outputs)
        {
            _classifier = new MinimumMeanDistanceClassifier(features, outputs);
        }

        /// <summary>
        /// Computes the specified input.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>
        /// The label for the class.
        /// </returns>
        public int Compute(Bitmap image)
        {
           ImageToArray converter = new ImageToArray(min: -1, max: +1);

            double[] input;
            converter.Convert(image, out input);

            int output = Compute(input);
            return output;
        }

        /// <summary>
        /// Computes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        /// The computed output.
        /// </returns>
        /// <exception cref="System.ArgumentException">Please Train the machine first.</exception>
        public int Compute(double[] input)
        {
            if (_classifier == null)
            {
                throw new ArgumentException("Please Train the machine first.");
            }

            double[] feature = _pca.Transform(input);
            int output = _classifier.Compute(feature);

            return output;
        }

        /// <summary>
        /// Extracts this _instance.
        /// </summary>
        /// <returns>
        /// The features.
        /// </returns>
        private double[][] Extract()
        {
            double[][] data = new double[this.Images.Count][];
            ImageToArray converter = new ImageToArray(min: -1, max: +1);

            int index = 0;
            foreach (Tuple<Bitmap, int> file in this.Images)
            {
                Bitmap image = file.Item1;
                converter.Convert(image, out data[index]);
                index++;
            }

            return data;
        }

    }
}
