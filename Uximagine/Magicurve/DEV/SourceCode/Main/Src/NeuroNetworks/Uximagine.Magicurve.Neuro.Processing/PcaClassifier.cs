using System;
using System.Collections.Generic;
using System.Drawing;
using Accord.Imaging.Converters;
using Accord.MachineLearning;
using Accord.Statistics.Analysis;

namespace Uximagine.Magicurve.Neuro.Processing
{
    /// <summary>
    /// Principal Component Analysis classifier
    /// </summary>
    public class PcaClassifier : IClassifier
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static PcaClassifier instance;

        /// <summary>
        /// The analysis.
        /// </summary>
        private PrincipalComponentAnalysis pca;

        /// <summary>
        /// The classifier.
        /// </summary>
        private MinimumMeanDistanceClassifier classifier;
        
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
        /// Gets or sets a value indicating whether this instance is trained.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is trained; otherwise, <c>false</c>.
        /// </value>
        public bool IsTrained
        {
            get; set;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="PcaClassifier"/> class from being created.
        /// </summary>
        private PcaClassifier()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>
        /// The single instance.
        /// </returns>
        public static PcaClassifier GetInstance()
        {
            if (instance == null)
            {
                instance = new PcaClassifier();
            }

            return instance;
        }

        /// <summary>
        /// Builds the PCA.
        /// </summary>
        private void BuildPca()
        {
            // Extract feature vectors
            double[][] fearures = this.Extract();

            // Create a new Principal Component Analysis object
            this.pca = new PrincipalComponentAnalysis(fearures, AnalysisMethod.Center);

            // Compute it
            this.pca.Compute();
        }

        /// <summary>
        /// Trains the machine.
        /// </summary>
        /// <param name="images">The images.</param>
        /// <param name="classesCount">The classes count.</param>
        public void TrainMachine(List<Tuple<Bitmap, int>> images, int classesCount)
        {
            this.Images = images;
            this.TrainMachine();
        }

        /// <summary>
        /// Trains the machine.
        /// </summary>
        public void TrainMachine()
        {
            if (this.Images == null)
            {
                throw new ArgumentException("Images cannot be null");
            }

            this.BuildPca();

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

                double[] feature = this.pca.Transform(input);

                inputs[index] = input;
                features[index] = feature;
                outputs[index] = label;
                index++;
            }

            this.TrainMachine(features, outputs);

            this.Images = null;
            this.IsTrained = true;
        }

        /// <summary>
        /// The train machine.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void TrainMachine(string url)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Trains the machine.
        /// </summary>
        /// <param name="features">The inputs.</param>
        /// <param name="outputs">The outputs.</param>
        public void TrainMachine(double[][] features, int[] outputs)
        {
            this.classifier = new MinimumMeanDistanceClassifier(features, outputs);
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

            int output = this.Compute(input);
            return output;
        }

        /// <summary>
        /// Computes the specified input.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The computed output.
        /// </returns>
        /// <exception cref="System.ArgumentException">Please Train the machine first.</exception>
        public int Compute(double[] input)
        {
            if (this.classifier == null)
            {
                throw new ArgumentException("Please Train the machine first.");
            }

            double[] feature = this.pca.Transform(input);
            double[] distances;
            int output = this.classifier.Compute(feature, out distances);

            return output;
        }

        /// <summary>
        /// Extracts this instance.
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
