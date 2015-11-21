using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math;
using Accord.Neuro;
using Accord.Neuro.Learning;
using Accord.Statistics.Kernels;
using AForge.Neuro;
using NUnit.Framework;
using Should;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.Neuro.Processing.Test
{
    /// <summary>
    /// Nural network tests.
    /// </summary>
    [TestFixture]
    public class NerualNetTests
    {
        /// <summary>
        /// Tests the xor.
        /// </summary>
        [Test]
        public static void TestXor()
        {
            double[][] inputs =
            {
                new double[] {0, 0}, new double[] { 0, 1},
                new double[] {1, 0}, new double[] { 1, 1}
            };

            double[][] outputs =
            {
                new double[] {0}, new double[] { 1},
                new double[] {1}, new double[] { 0}
            };

            ActivationNetwork network = new ActivationNetwork(
                new SigmoidFunction(2),
                2, // two inputs in the network
                2, // two neurons in the first layer
                1);

            var teacher = new LevenbergMarquardtLearning(network);

            for (int i = 0; i < 10; i++)
            {
                double error = teacher.RunEpoch(inputs, outputs);
            }

            double[] output = network.Compute(new double[] { 0, 0 });
            int answer;

            double result = output.Max(out answer);

            answer.ShouldEqual(0);
        }

        /// <summary>
        /// Tests the training.
        /// </summary>
        [Test]
        public void TestMutipleCalssTraining()
        {
            const int numberOfInputs = 3;
            const int numberOfClasses = 4;
            const int hiddenNeurons = 5;

            // Those are the input vectors and their expected class labels
            // that we expect our network to learn.
            // 
            double[][] input =
            {
                new double[] {-1, -1, -1}, // 0
                new double[] {-1, 1, -1}, // 1
                new double[] {1, -1, -1}, // 1
                new double[] {1, 1, -1}, // 0
                new double[] {-1, -1, 1}, // 2
                new double[] {-1, 1, 1}, // 3
                new double[] {1, -1, 1}, // 3
                new double[] {1, 1, 1} // 2
            };

            int[] labels =
            {
                0,
                1,
                1,
                0,
                2,
                3,
                3,
                2,
            };

            // In order to perform multi-class classification, we have to select a 
            // decision strategy in order to be able to interpret neural network 
            // outputs as labels. For this, we will be expanding our 4 possible class
            // labels into 4-dimensional output vectors where one single dimension 
            // corresponding to a label will contain the value +1 and -1 otherwise.

            double[][] outputs = Accord.Statistics.Tools
                .Expand(labels, numberOfClasses, -1, 1);

            // Next we can proceed to create our network
            var function = new BipolarSigmoidFunction(2);
            var network = new ActivationNetwork(function,
                numberOfInputs, hiddenNeurons, numberOfClasses);

            // Heuristically randomize the network
            new NguyenWidrow(network).Randomize();

            // Create the learning algorithm
            var teacher = new LevenbergMarquardtLearning(network);

            // Teach the network for 100 iterations:
            double error = double.PositiveInfinity;

            for (int i = 0; i < 100; i++)
                error = teacher.RunEpoch(input, outputs);

            // At this point, the network should be able to 
            // perfectly classify the training input points.

            for (int i = 0; i < input.Length; i++)
            {
                int answer;
                double[] output = network.Compute(input[i]);
                double response = output.Max(out answer);

                int expected = labels[i];

                answer.ShouldEqual(expected);

                // at this point, the variables 'answer' and
                // 'expected' should contain the same value.
            }
        }

        /// <summary>
        /// Tests the training.
        /// </summary>
        [Test]
        public void TestVectorMachineTraining()
        {
            // Sample input data
            double[][] inputs =
            {
                new double[] { -1, 3, 2 },
                new double[] { 10, 82, 4 },
                new double[] { 10, 15, 4 },
                new double[] { 0, 0, 1 }
            };

            // Output for each of the inputs
            int[] outputs = { 0, 3, 1, 2 };


            // Create a new polynomial kernel
            IKernel kernel = new Polynomial(2);

            // Create a new Multi-class Support Vector Machine with one input,
            //  using the linear kernel and for four disjoint classes.
            var machine = new MulticlassSupportVectorMachine(inputs: 3, kernel: kernel, classes: 4);

            // Create the Multi-class learning algorithm for the machine
            var teacher = new MulticlassSupportVectorLearning(machine, inputs, outputs);

            // Configure the learning algorithm to use SMO to train the
            //  underlying SVMs in each of the binary class subproblems.
            teacher.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                new SequentialMinimalOptimization(svm, classInputs, classOutputs);

            // Run the learning algorithm
            double error = teacher.Run(); // output should be 0

            // Compute the decision output for one of the input vectors
            int decision = machine.Compute(new double[] { -1, 3, 2 });

            decision.ShouldEqual(0);
        }

        /// <summary>
        /// Tests the training.
        /// </summary>
        [Test]
        public void TestFeatureVectorTraining()
        {
            // Sample input data
            // pointsCount, linesCount, DistinctAngleCount, horizantalLineCount, VerticalLineCount 
            double[][] inputs =
            {
                new double[] { 0, 0, 0, 0, 0 },           // Radio
                new double[] { 4, 4, 1, 2, 2 },           // Button
                new double[] { 5, 6, 4, 2, 2 },           // Combo
                new double[] { 6, 5, 1, 2, 3 },           // Text Box
                new double[] { 6, 5, 1, 3, 2 },           // Paragraph
                new double[] { 5, 5, 2, 3, 2 }            // IFrame
            };

            // Output for each of the inputs
            int[] outputs = { 0, 1, 2, 3, 4, 5 };


            // Create a new polynomial kernel
            IKernel kernel = new Polynomial(2);

            // Create a new Multi-class Support Vector Machine with one input,
            //  using the linear kernel and for four disjoint classes.
            var machine = new MulticlassSupportVectorMachine(inputs: 5, kernel: kernel, classes: 6);

            // Create the Multi-class learning algorithm for the machine
            var teacher = new MulticlassSupportVectorLearning(machine, inputs, outputs);

            // Configure the learning algorithm to use SMO to train the
            //  underlying SVMs in each of the binary class subproblems.
            teacher.Algorithm = (svm, classInputs, classOutputs, i, j) =>
                new SequentialMinimalOptimization(svm, classInputs, classOutputs);

            // Run the learning algorithm
            double error = teacher.Run(); // output should be 0

            // Compute the decision output for one of the input vectors
            int decision = machine.Compute(new double[] { 6, 5, 1, 2, 3 });

            decision.ShouldEqual(3);
        }

        /// <summary>
        /// Gets the features.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <returns>
        /// The features
        /// </returns>
        public double[] GetFeatures(Control control)
        {
            double[] features = new double[control.EdgePoints.Count];
            for (int i = 0; i < control.EdgePoints.Count; i++)
                features[i] = control.EdgePoints[i].EuclideanNorm();

            return features;
        }
    }
}
