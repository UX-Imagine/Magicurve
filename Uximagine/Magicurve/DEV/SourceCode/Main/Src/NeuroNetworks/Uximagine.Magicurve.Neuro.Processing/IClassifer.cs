namespace Uximagine.Magicurve.Neuro.Processing
{
    /// <summary>
    /// The interface for classifiers.
    /// </summary>
    public interface IClassifer
    {
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
        /// Computes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        int Compute(double[] input);

    }
}