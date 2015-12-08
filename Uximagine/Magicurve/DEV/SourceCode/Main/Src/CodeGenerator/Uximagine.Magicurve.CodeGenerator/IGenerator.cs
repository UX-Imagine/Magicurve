using System.Collections.Generic;
using Uximagine.Magicurve.Core.Shapes;

namespace Uximagine.Magicurve.CodeGenerator
{
    /// <summary>
    /// The Generator interface.
    /// </summary>
    public interface IGenerator
    {
        /// <summary>
        /// Creates the HTML code.
        /// </summary>
        /// <param name="controls">The controls.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>
        /// The HTML code.
        /// </returns>
        string CreateHtmlCode(List<Control> controls, double width, double height);
    }
}
