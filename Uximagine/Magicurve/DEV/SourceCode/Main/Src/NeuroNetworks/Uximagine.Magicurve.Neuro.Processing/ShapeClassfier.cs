using System;
using System.Collections.Generic;
using System.Drawing;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;

namespace Uximagine.Magicurve.Neuro.Processing
{
    /// <summary>
    /// The shape classifier.
    /// </summary>
    public class ShapeClassfier : IClassifier
    {
        // Sample input data
        // pointsCount, linesCount, DistinctAngleCount, horizantalLineCount, VerticalLineCount 
        private MulticlassSupportVectorMachine Machine { get; set; }

        /// <summary>
        /// Gets or sets the number of inputs.
        /// </summary>
        /// <value>
        /// The number of inputs.
        /// </value>
        public int NumberOfInputs { get; set; }

        /// <summary>
        /// Gets or sets the number of classes.
        /// </summary>
        /// <value>
        /// The number of classes.
        /// </value>
        public int NumberOfClasses { get; set; }

        /// <summary>
        /// Gets or sets the outputs.
        /// </summary>
        /// <value>
        /// The outputs.
        /// </value>
        public int[] Outputs { get; set; }

        /// <summary>
        /// Gets or sets the inputs.
        /// </summary>
        /// <value>
        /// The inputs.
        /// </value>
        public double[][] Inputs { get; set; }

        /// <summary>
        /// The classifier.
        /// </summary>
        private static ShapeClassfier _classifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeClassfier"/> class.
        /// </summary>
        /// <param name="nInputs">The number of inputs.</param>
        /// <param name="classes">The number of classes.</param>
        private ShapeClassfier(int nInputs, int classes)
        {
            this.NumberOfInputs = nInputs;
            this.NumberOfClasses = classes;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="nInputs">
        /// The no of inputs.</param>
        /// <param name="classes">
        /// The classes.
        /// </param>
        /// <returns>
        /// The instance.
        /// </returns>
        public static IClassifier GetInstance(int nInputs, int classes)
        {
            if (_classifier == null)
            {
                _classifier = new ShapeClassfier(nInputs, classes);
            }

            return _classifier;
        }

        public void TrainMachine(List<Tuple<Bitmap, int>> images, int classesCount)
        {
            throw new NotImplementedException();
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

        public int Compute(Bitmap input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Trains the machine.
        /// </summary>
        /// <param name="inputs">
        /// The inputs.
        /// </param>
        /// <param name="outputs">
        /// The outputs.
        /// </param>
        public void TrainMachine(double[][] inputs, int[] outputs)
        {
            this.Inputs = inputs;
            this.Outputs = outputs;
            this.TrainMachine();
        }

        public bool IsTrained { get; set; }

        /// <summary>
        /// Trains the Machine.
        /// </summary>
        public void TrainMachine()
        {
            if (this.Inputs == null)
            {
                throw new ArgumentException("Inputs cannot be null");
            }

            if (this.Outputs == null)
            {
                throw new ArgumentException("outputs cannot be null");
            }

            // Create a new polynomial kernel
            IKernel kernel = new Polynomial(2);

            // Create a new Multi-class Support Vector Machine with one input,
            //  using the linear kernel and for four disjoint classes.
            if (Machine == null)
            {
                Machine = new MulticlassSupportVectorMachine(inputs: this.NumberOfInputs, kernel: kernel, classes: this.NumberOfClasses);
            }

            // Create the Multi-class learning algorithm for the Machine
            var teacher = new MulticlassSupportVectorLearning(Machine, Inputs, Outputs);

            // Configure the learning algorithm to use SMO to train the
            //  underlying SVMs in each of the binary class subproblems.
            teacher.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                new SequentialMinimalOptimization(svm, classInputs, classOutputs);

            // Run the learning algorithm
            teacher.Run();

        }
    }
}
