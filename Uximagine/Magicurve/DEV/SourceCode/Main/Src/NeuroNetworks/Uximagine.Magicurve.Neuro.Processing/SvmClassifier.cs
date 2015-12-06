using System;
using System.Collections.Generic;
using System.Drawing;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;

namespace Uximagine.Magicurve.Neuro.Processing
{
    public class SvmClassifier : IClassifer
    {
        // Sample input data
        // pointsCount, linesCount, DistinctAngleCount, horizantalLineCount, VerticalLineCount 
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
        private static SvmClassifier _classifier;

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
        /// Gets or sets the size of the sample.
        /// </summary>
        /// <value>
        /// The size of the sample.
        /// </value>
        public int SampleSize = 32;

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
        /// Initializes a new instance of the <see cref="ShapeClassfier"/> class.
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
            if (_classifier == null)
            {
                _classifier = new SvmClassifier();
            }

            return _classifier;
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
            double[] input = Extract(image);
            int decision = Machine.Compute(input);
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
            int decision = Machine.Compute(input);
            return decision;
        }

        public void TrainMachine(List<Tuple<Bitmap, int>> data, int classCount)
        {
            Inputs = new List<double[]>();
            Outputs = new List<int>();

            foreach (Tuple<Bitmap, int> tuple in data)
            {
                double[] features = Extract(tuple.Item1);
                if (features != null && features.Length != 0)
                {
                    Inputs.Add(features);
                    Outputs.Add(tuple.Item2);
                }
            }

            this.ClassCount = classCount;
           
            this.TrainMachine(Inputs.ToArray(), Outputs.ToArray());
        }


        /// <summary>
        /// Extracts the specified BMP.
        /// </summary>
        /// <param name="bmp">
        /// The BMP.
        /// </param>
        /// <returns>
        /// Extracted features.
        /// </returns>
        private double[] Extract(Bitmap bmp)
        {
            double[] features = new double[SampleSize * SampleSize];
            for (int i = 0; i < SampleSize; i++)
                for (int j = 0; j < SampleSize; j++)
                    features[i * SampleSize + j] = (bmp.GetPixel(j, i).R == 255) ? 0 : 1;

            return features;
        }

        /// <summary>
        /// Trains the machine.
        /// </summary>
        public void TrainMachine()
        {
            TrainMachine(Samples, ClassCount);
        }

        /// <summary>
        /// Trains the Machine.
        /// </summary>
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
            if (Machine == null)
            {
                int inputCount = inputs.GetUpperBound(0) + 1;
                int inputCount2 = inputs.GetUpperBound(1) + 1;
                Machine = new MulticlassSupportVectorMachine(inputs: inputCount, kernel: kernel, classes: ClassCount);
            }

            // Create the Multi-class learning algorithm for the Machine
            var teacher = new MulticlassSupportVectorLearning(Machine, inputs, outputs);

            // Configure the learning algorithm to use SMO to train the
            //  underlying SVMs in each of the binary class subproblems.
            teacher.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                new SequentialMinimalOptimization(svm, classInputs, classOutputs);

            // Run the learning algorithm
            teacher.Run();

            Machine.Save("machine.nn");

            IsTrained = true;

        }
    }
}
