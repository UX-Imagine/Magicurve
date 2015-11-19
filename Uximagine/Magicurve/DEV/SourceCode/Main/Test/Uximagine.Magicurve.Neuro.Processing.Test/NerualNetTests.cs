using System;
using Accord.Math;
using Accord.Neuro;
using Accord.Neuro.Learning;
using AForge.Neuro;
using AForge.Neuro.Learning;
using NUnit.Framework;
using Should;

namespace Uximagine.Magicurve.Neuro.Processing.Test
{
    /// <summary>
    /// Nural network tests.
    /// </summary>
    [TestFixture]
    public class NerualNetTests
    {
        /// <summary>
        /// Tests the sample.
        /// </summary>
        [Test]
        public void TestSample(int samples, int classesCount, double learningRate)
        {
            int numberOfInputs = 3;
            int numberOfClasses = 4;
            int hiddenNerurons = 5;

            double[][] input =
            {
                new double[] {-1, -1, 1}, // 0
                new double[] {-1, 1, -1}, // 1
                new double[] {1, -1, -1}, // 1
            };

        }

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
        public void TestTraining()
        {
            int numberOfInputs = 3;
            int numberOfClasses = 4;
            int hiddenNeurons = 5;

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

            // Teach the network for 10 iterations:
            double error = Double.PositiveInfinity;

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
    }
}
