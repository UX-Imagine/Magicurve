using System.Collections.Generic;
using System.Drawing;
using AForge;
using Uximagine.Magicurve.Core.Models;

namespace Uximagine.Magicurve.Image.Processing.ShapeCheckers
{
    /// <summary>
    /// The shape checker interface.
    /// </summary>
    public interface IShapeChecker
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        int X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        int Y { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        double Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        double Height { get; set; }

        /// <summary>
        /// Gets the type of the control.
        /// </summary>
        /// <param name="edgePoints">
        /// The edge points.
        /// </param>
        /// <returns>
        /// The content type
        /// </returns>
        ControlType GetControlType(List<IntPoint> edgePoints);

        /// <summary>
        /// Gets the shape corners.
        /// </summary>
        /// <param name="edgePoints">The edge points.</param>
        /// <returns>
        /// The shape corners.
        /// </returns>
        List<IntPoint> GetShapeCorners(List<IntPoint> edgePoints);

        /// <summary>
        /// Gets the type of the control.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="edgePoints">The edge points.</param>
        /// <returns>
        /// The control type
        /// </returns>
        ControlType GetControlType(Bitmap original, List<IntPoint> edgePoints);

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="edgePoints">
        /// The edge points.
        /// </param>
        void SetProperties(List<IntPoint> edgePoints);

    }
}