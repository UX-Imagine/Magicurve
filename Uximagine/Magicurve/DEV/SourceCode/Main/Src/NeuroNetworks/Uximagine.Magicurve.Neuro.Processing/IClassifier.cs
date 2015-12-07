using System;
using System.Collections.Generic;
using System.Drawing;

namespace Uximagine.Magicurve.Neuro.Processing
{
    /// <summary>
    /// The interface for classifiers.
    /// </summary>
    public interface IClassifier
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is trained.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is trained; otherwise, <c>false</c>.
        /// </value>
        bool IsTrained { get; set; }

        /// <summary>
        /// Trains the machine.
        /// </summary>
        void TrainMachine();

        /// <summary>
        /// Trains the machine.
        /// </summary>
        /// <param name="features">The inputs.</param>
        /// <param name="outputs">The outputs.</param>
        void TrainMachine(double[][] features, int[] outputs);

        /// <summary>
        /// Trains the machine.
        /// </summary>
        /// <param name="images">The images.</param>
        /// <param name="classesCount">The classes count.</param>
        void TrainMachine(List<Tuple<Bitmap, int>> images, int classesCount);

        /// <summary>
        /// Computes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        /// The corresponding class.
        /// </returns>
        int Compute(double[] input);

        /// <summary>
        /// Computes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        /// The corresponding class.
        /// </returns>
        int Compute(Bitmap input);
    }
}