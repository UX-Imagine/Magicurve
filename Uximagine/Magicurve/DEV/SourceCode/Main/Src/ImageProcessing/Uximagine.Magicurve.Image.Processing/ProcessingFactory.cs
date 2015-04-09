namespace Uximagine.Magicurve.Image.Processing
{
    using Uximagine.Magicurve.Core.Reflection;
    using Uximagine.Magicurve.Image.Processing.Detectors;
    using Uximagine.Magicurve.Image.Processing.Matchers;

    /// <summary>
    /// The matcher factory
    /// </summary>
    public static class ProcessingFactory
    {
        /// <summary>
        /// Gets the matcher.
        /// </summary>
        /// <returns>
        /// The matcher plug-in.
        /// </returns>
        public static IMatcher GetMatcher()
        {
            return ObjectFactory.GetInstance<IMatcher>(
                 ConfigurationData.MatcherImplementationPluginName);
        }

        /// <summary>
        /// Gets the detector.
        /// </summary>
        /// <returns>
        /// The detector plug-in
        /// </returns>
        public static IEdgeDetector GetEdgeDetector()
        {
            return ObjectFactory.GetInstance<IEdgeDetector>(
                 ConfigurationData.EdgeDetectorImplementationPluginName);
        }

        /// <summary>
        /// Gets the detector.
        /// </summary>
        /// <returns>
        /// The detector plug-in
        /// </returns>
        public static IBlobDetector GetBlobDetector()
        {
            return ObjectFactory.GetInstance<IBlobDetector>(
                 ConfigurationData.BlobDetectorImplementationPluginName);
        }
    }
}
