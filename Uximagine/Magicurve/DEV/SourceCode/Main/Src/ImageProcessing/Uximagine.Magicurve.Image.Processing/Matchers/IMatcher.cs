using System.Drawing;

namespace Uximagine.Magicurve.Image.Processing.Matchers
{
    /// <summary>
    /// Matcher interface.
    /// </summary>
    public interface IMatcher
    {
        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>
        /// The template.
        /// </value>
        Bitmap Template { get; set; }

        /// <summary>
        /// Matches the specified source.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <returns>
        /// Matched results.
        /// </returns>
        Bitmap Match(Bitmap source);
    }
}
