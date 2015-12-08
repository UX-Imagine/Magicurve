using System;
using System.Collections.Generic;
using System.Drawing;
using Accord.Imaging.Converters;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;

namespace Uximagine.Magicurve.Neuro.Processing
{
    /// <summary>
    /// The SVM classifier.
    /// </summary>
    public class SvmClassifier : IClassifier
    {
        /// <summary>
        /// Gets or sets the machine.
        /// </summary>
        private MulticlassSupportVectorMachine Machine { get; set; }

        /// <summary>
        /// Gets or sets the outputs.
        /// </summary>
        /// <value>
        /// The outputs.
        /// </value>
        public List<int> Outputs { get; set; }

        /// <summary>
        /// Gets or sets the inputs.
        /// </summary>
        /// <value>
        /// The inputs.
        /// </value>
        public List<double[]> Inputs { get; set; }

        /// <summary>
        /// The classifier.
        /// </summary>
        private static SvmClassifier classifier;

        /// <summary>
        /// Gets or sets the samples.
        /// </summary>
        /// <value>
        /// The samples.
        /// </value>
        public List<Tuple<Bitmap, int>> Samples
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the class count.
        /// </summary>
        /// <value>
        /// The class count.
        /// </value>
        public int ClassCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is trained.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is trained; otherwise, <c>false</c>.
        /// </value>
        public bool IsTrained { get; set; }

        /// <summary>
        /// The inputs count.
        /// </summary>
        private int inputsCount;
        
        /// <summary>
        /// Prevents a default instance of the <see cref="SvmClassifier"/> class from being created.
        /// </summary>
        private SvmClassifier()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>
        /// The instance.
        /// </returns>
        public static SvmClassifier GetInstance()
        {
            if (classifier == null)
            {
                classifier = new SvmClassifier();
            }

            return classifier;
        }

        /// <summary>
        /// Computes the specified image.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The decision.
        /// </returns>
        public int Compute(Bitmap image)
        {
            // Compute the decision output for one of the input vectors
            double[] input = this.Extract(image);
            int decision = this.Machine.Compute(input);
            return decision;
        }

        /// <summary>
        /// Computes the specified input.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The computed value.
        /// </returns>
        public int Compute(double[] input)
        {
            // Compute the decision output for one of the input vectors
            int decision = this.Machine.Compute(input);
            return decision;
        }

        /// <summary>
        /// Trains the machine.
        /// </summary>
        public void TrainMachine()
        {
            if (ConfigurationData.LoadMachineFromFile)
            {
                this.Machine = MulticlassSupportVectorMachine.Load(ConfigurationData.MachineUrl);
                this.IsTrained = true;
            }
            else
            {
                this.TrainMachine(this.Samples, this.ClassCount);
            }
        }

        /// <summary>
        /// Trains the machine.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="classCount">The class count.</param>
        public void TrainMachine(List<Tuple<Bitmap, int>> data, int classCount)
        {
            this.Inputs = new List<double[]>();
            this.Outputs = new List<int>();

            foreach (Tuple<Bitmap, int> tuple in data)
            {
                double[] features = this.Extract(tuple.Item1);
                if (features != null && features.Length != 0)
                {
                    this.Inputs.Add(features);
                    this.Outputs.Add(tuple.Item2);
                }
            }

            this.ClassCount = classCount;
           
            this.TrainMachine(this.Inputs.ToArray(), this.Outputs.ToArray());
        }

        /// <summary>
        /// Trains the Machine.
        /// </summary>
        /// <param name="inputs">The inputs.</param>
        /// <param name="outputs">The outputs.</param>
        /// <exception cref="System.ArgumentException">
        /// Inputs cannot be null
        /// or
        /// outputs cannot be null
        /// </exception>
        public void TrainMachine(double[][] inputs, int[] outputs)
        {
            if (inputs == null)
            {
                throw new ArgumentException("Inputs cannot be null");
            }

            if (outputs == null)
            {
                throw new ArgumentException("outputs cannot be null");
            }

            // Create a new polynomial kernel
            IKernel kernel = new Polynomial(2);

            // Create a new Multi-class Support Vector Machine with one input,
            //  using the linear kernel and for four disjoint classes.
            if (this.Machine == null)
            {
                this.Machine = new MulticlassSupportVectorMachine(inputs: this.inputsCount, kernel: kernel, classes: this.ClassCount);
            }

            // Create the Multi-class learning algorithm for the Machine
            var teacher = new MulticlassSupportVectorLearning(this.Machine, inputs, outputs);

            // Configure the learning algorithm to use SMO to train the
            //  underlying SVMs in each of the binary class subproblems.
            teacher.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                new SequentialMinimalOptimization(svm, classInputs, classOutputs);

            // Run the learning algorithm
            teacher.Run();

            this.Machine.Save("E:/machine.nn");

            this.IsTrained = true;

        }

        /// <summary>
        /// Extracts the specified BMP.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>
        /// Extracted features.
        /// </returns>
        private double[] Extract(Bitmap image)
        {
            ImageToArray converter = new ImageToArray(min: -1, max: +1);

            double[] input;
            converter.Convert(image, out input);

            this.inputsCount = input.Length;

            return input;
        }
    }
}
