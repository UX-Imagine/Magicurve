using Uximagine.Magicurve.Image.Processing.ShapeCheckers;
using Uximagine.Magicurve.Neuro.Processing;

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

        /// <summary>
        /// Gets the shape checker.
        /// </summary>
        /// <returns>
        /// The shape checker.
        /// </returns>
        public static IShapeChecker GetShapeChecker()
        {
            return ObjectFactory.GetInstance<IShapeChecker>(
                 ConfigurationData.ShapeCheckerImplementationPluginName);
        }

        /// <summary>
        /// Gets the classifier.
        /// </summary>
        /// <returns>
        /// The classifier.
        /// </returns>
        public static IClassifier GetClassifier()
        {
            return SvmClassifier.GetInstance();
        }
    }
}
