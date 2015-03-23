namespace Uximagine.Magicurve.Image.Processing.Matchers
{
    /// <summary>
    /// The matcher factory
    /// </summary>
    public static class MatcherFactory
    {
        /// <summary>
        /// Gets the block matcher.
        /// </summary>
        /// <returns>
        /// The block matcher.
        /// </returns>
        public static IMatcher GetBlockMatcher()
        {
            return new BlockMatcher();
        }


        /// <summary>
        /// Gets the template matcher.
        /// </summary>
        /// <returns>
        /// The template matcher.
        /// </returns>
        public static IMatcher GetTemplateMatcher()
        {
            return new TemplateMatcher();
        }
    }
}
