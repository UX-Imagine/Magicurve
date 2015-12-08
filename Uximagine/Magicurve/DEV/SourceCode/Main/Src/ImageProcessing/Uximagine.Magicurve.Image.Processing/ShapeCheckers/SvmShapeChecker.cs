using System;
using System.Collections.Generic;
using System.Drawing;
using AForge;
using Uximagine.Magicurve.Core.Models;

namespace Uximagine.Magicurve.Image.Processing.ShapeCheckers
{
    /// <summary>
    /// The SVM shape checker.
    /// </summary>
    public class SvmShapeChecker : IShapeChecker
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public double Height { get; set; }

        /// <summary>
        /// Gets the type of the control.
        /// </summary>
        /// <param name="edgePoints">The edge points.</param>
        /// <returns>
        /// The content type
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// This function is not implemented.
        /// </exception>
        public ControlType GetControlType(List<IntPoint> edgePoints)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the shape corners.
        /// </summary>
        /// <param name="edgePoints">
        /// The edge points.
        /// </param>
        /// <returns>
        /// The shape corners.
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// This function is not implemented.
        /// </exception>
        public List<IntPoint> GetShapeCorners(List<IntPoint> edgePoints)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the type of the control.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <param name="edgePoints">The edge points.</param>
        /// <returns>
        /// The control type
        /// </returns>
        /// <exception cref="System.NotImplementedException">
        /// This function is not implemented.
        /// </exception>
        public ControlType GetControlType(Bitmap original, List<IntPoint> edgePoints)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="edgePoints">The edge points.</param>
        /// <exception cref="System.NotImplementedException">
        /// This function is not implemented.
        /// </exception>
        public void SetProperties(List<IntPoint> edgePoints)
        {
            throw new NotImplementedException();
        }
    }
}
