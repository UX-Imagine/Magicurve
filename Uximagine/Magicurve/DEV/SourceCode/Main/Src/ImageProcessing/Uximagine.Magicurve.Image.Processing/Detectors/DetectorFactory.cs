namespace Uximagine.Magicurve.Image.Processing.Detectors
{
    /// <summary>
    /// Factory for detectors.
    /// </summary>
    public static class DetectorFactory
    {
        /// <summary>
        /// Gets the BLOB detector.
        /// </summary>
        /// <returns>
        /// The blob detector.
        /// </returns>
        public static IDetector GetBlobDetector()
        {
            return new BlobDetector();
        }

        /// <summary>
        /// Gets the edge detector.
        /// </summary>
        /// <returns>
        /// The edge detector.
        /// </returns>
        public static IDetector GetEdgeDetector()
        {
            return new EdgeDetector();
        }
    }
}
